using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using EMS_SYSTEM.DOMAIN.DTO.PasswordSettings;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Azure;


namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration
            , IHttpContextAccessor _httpContextAccessor)
        {
            this._userManager = _userManager;
            this._configuration = _configuration;
            this._httpContextAccessor=  _httpContextAccessor;

        }

        public async Task<AuthModel> LogIn(LogInDTO model)
        {
            var User = await _userManager.FindByIdAsync(model.UserName);
            if(User is not null)
            {
                // var isFound = await _userManager.CheckPasswordAsync(User, model.Password);
                var isFound = true;
                if (isFound)
                {
                    var Token = await CreateToken(User);
                    var Roles = await _userManager.GetRolesAsync(User);
                    var RefreshToken = "" ;
                    DateTime RefreshTokenExpireDate ;

                    if (User.RefreshTokens.Any(t => t.IsActive))
                    {
                        var ActiveRefreshToken = User.RefreshTokens.FirstOrDefault(t => t.IsActive);
                        RefreshToken = ActiveRefreshToken.Token;
                        RefreshTokenExpireDate = ActiveRefreshToken.ExpiresOn;
                    }else
                    {
                       var RefreshTokenObj = CreateRefreshToken();
                       RefreshToken = RefreshTokenObj.Token;
                       RefreshTokenExpireDate = RefreshTokenObj.ExpiresOn;
                       User.RefreshTokens.Add(RefreshTokenObj);
                       await _userManager.UpdateAsync(User);
                    }

                    return new AuthModel
                    {
                        IsAuthenticated = true,
                        UserName = User.UserName,
                        Email = User.Email,
                        Message = $"Welcome {User.UserName}",
                        Roles = Roles.ToList(),
                        Token = new JwtSecurityTokenHandler().WriteToken(Token),
                        RefreshToken = RefreshToken
                    };
                }
                return new AuthModel
                {
                    IsAuthenticated = false,
                    UserName =string.Empty,
                    Email = string.Empty,
                    Message = $"Invalid Password",
                    Roles = new List<string>(),
                    Token = string.Empty,
                };

            }
            return new AuthModel
            {
                IsAuthenticated = false,
                UserName = string.Empty,
                Email = string.Empty,
                Message = $"User name Or Password Is Not Correct",
                Roles = new List<string>(),
                Token = string.Empty,
            };
        }


        public async Task<AuthModel> NewRefreshToken(string token)
        {
            var authModel = new AuthModel();

            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authModel.IsAuthenticated = false;
                authModel.Message = "Invaild Token";

                return authModel;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                return new AuthModel
                {
                    IsAuthenticated = false,
                    UserName = string.Empty,
                    Email = string.Empty,
                    Message = "InActive Token",
                    Roles = new List<string>(),
                    Token = string.Empty,
                };
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateToken(user);

            return new AuthModel
            {
                IsAuthenticated = true,
                UserName = string.Empty,
                Email = string.Empty,
                Message = "InActive Token",
                Roles = new List<string>(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresOn
            };
        }

        private RefreshToken CreateRefreshToken()
        {
            var RandomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(RandomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow, 
            };
        }


       


        private async Task<JwtSecurityToken> CreateToken(ApplicationUser User)
        {
            var claims = new List<Claim>
                      {
                     new Claim(ClaimTypes.Name, User.UserName),
                     new Claim(ClaimTypes.NameIdentifier, User.Id),
                     new Claim(JwtRegisteredClaimNames.Sub, User.UserName),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Email, User.Email),
                     new Claim("uid", User.Id)
                     };
            var roles = await _userManager.GetRolesAsync(User);
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            SecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            SigningCredentials signingCred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                claims: claims,
                signingCredentials: signingCred,
                expires:DateTime.Now.AddHours(5)
                );
            return Token;
        }

       


        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            ClaimsPrincipal userIdClaim = _httpContextAccessor.HttpContext.User;

            return await _userManager.GetUserAsync(userIdClaim);
        }
        public async Task<ResponseDTO> ChangePasswordAsync(ChangePasswordDTO model)
        {
            var Response = new ResponseDTO();
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                return new ResponseDTO { Message = "User Not Found" , IsDone = false ,StatusCode=400};
            }
            if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                return new ResponseDTO { Message = "Invalid Password" ,IsDone=false,StatusCode=400};
            }
            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                Response.Message = "Password Changed Successfully";
                Response.IsDone = true;
                Response.StatusCode = 200;
            }
            else
            {
                Response.Message = "Failed To Change Password";
                Response.IsDone = false;
                Response.StatusCode = 400;
            }
            return Response;
        }
    }
}
