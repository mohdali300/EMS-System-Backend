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

        [HttpGet("GetStudent/{Id}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
              return Ok(await _studentService.GetStudentByID(Id));            
        }
    }
}
