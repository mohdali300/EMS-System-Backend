using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using EMS_SYSTEM.DOMAIN.Models;
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

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration)
        {
            this._userManager = _userManager;
            this._configuration = _configuration;

        }

        public async Task<AuthModel> LogIn(LogInDTO model)
        {
            var User = await _userManager.FindByNameAsync(model.UserName);
            if(User is not null)
            {
                var isFound = await _userManager.CheckPasswordAsync(User, model.Password);
                if (isFound)
                {
                    var Token = await CreateToken(User);
                    var Roles = await _userManager.GetRolesAsync(User);
                    return new AuthModel
                    {
                        IsAuthenticated = true,
                        UserName=User.UserName,
                        Email=User.Email,
                        Message=$"Welcome {User.UserName}",
                        Roles=Roles.ToList(),
                        Token=new JwtSecurityTokenHandler().WriteToken(Token),  
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
        public async Task<JwtSecurityToken> CreateToken(ApplicationUser User)
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
                signingCredentials: signingCred
                );
            return Token;
        }
        
    }
}
