using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private ResponseDTO _responseDTO;
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService _facultyService)
        {
            _responseDTO = new ResponseDTO();
            this._facultyService = _facultyService;
        }
        
        [HttpGet("GetFaculty/{Id}")]
        public async Task<IActionResult> GetFacultyById(int Id)
        {
            if (ModelState.IsValid)
            {
                _responseDTO = await  _facultyService.GetFacultyDataByID(Id);
                if (_responseDTO.IsDone)
                {
                    return StatusCode(_responseDTO.StatusCode, _responseDTO.Model);
                }
                return StatusCode(_responseDTO.StatusCode, _responseDTO.Message);
            }
            return BadRequest(ModelState);
        }
        
    }
}
