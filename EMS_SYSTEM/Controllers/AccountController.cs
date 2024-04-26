using System.Security.Claims;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using EMS_SYSTEM.DOMAIN.DTO.PasswordSettings;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(IAccountService _accountService , IHttpContextAccessor httpContextAccessor ,UserManager<ApplicationUser> userManager)
        {
            this._accountService=_accountService;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody]LogInDTO model)
        {
            if (ModelState.IsValid)
            {
                var Response= await _accountService.LogIn(model);
                if (Response.IsAuthenticated==true)
                {
                    return Ok(new {Response.Message,Response.Token});
                }
                return BadRequest(new {Response.Message});
            }
            return BadRequest(ModelState);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO ChangePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            // Get Current User
            ClaimsPrincipal userIdClaim = httpContextAccessor.HttpContext.User;
            var user = await userManager.GetUserAsync(userIdClaim);
            //User Not Found
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "User doesn't Exists" });
            }
            var result = await userManager.ChangePasswordAsync(user, ChangePassword.CurrentPassword!, ChangePassword.NewPassword!);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDTO { Status = "Success", Message = "Password Changed Successfully" });

        }
    }
}
