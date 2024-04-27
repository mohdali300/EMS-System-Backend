using System.Security.Claims;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using EMS_SYSTEM.DOMAIN.DTO.PasswordSettings;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            if (ModelState.IsValid)
            {
                var Response = await _accountService.ChangePasswordAsync(ChangePassword);
                if (Response.IsDone == true)
                {
                    return StatusCode(Response.StatusCode, Response.Message);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }        
            return BadRequest(ModelState);

        }
    }
}
