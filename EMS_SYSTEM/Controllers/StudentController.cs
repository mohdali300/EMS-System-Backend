using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService _studentService) 
        {
            this._studentService = _studentService;
        }
        [HttpGet("GetData/(:id)")]
        public async Task<IActionResult> GetData(string Id)
        {

            return Ok();
        }
    }
}
