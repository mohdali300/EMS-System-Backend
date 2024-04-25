using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO.LogIn;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService _accountService)
        {
            this._accountService=_accountService;
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
    }
}
