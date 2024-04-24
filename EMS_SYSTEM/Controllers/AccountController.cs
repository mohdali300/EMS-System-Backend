using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("LogIn")]
        public async Task<IActionResult> LogIn()
        {
            return Ok();
        }
    }
}
