    using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.APPLICATION.Repositories.Services.UnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]// For All Roles
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
        [HttpGet("subjects")]
        [Authorize]
        public async Task<IActionResult> GetSubjects([FromQuery] FacultyHieryicalDTO hieryicalDTO)
        {
            if (ModelState.IsValid)
            {
                _responseDTO = await _facultyService.GetSubjects(hieryicalDTO);
                if (_responseDTO.IsDone)
                {
                    return StatusCode(_responseDTO.StatusCode, _responseDTO.Model);
                }
                return StatusCode(_responseDTO.StatusCode, _responseDTO.Message);
            }
            return BadRequest(ModelState);
        }


        [HttpGet("FacultyCommitteesDetails")]
 
        public async Task<IActionResult> GetFacultyCommitteesDetails(int FacultyID)
        {
            if (ModelState.IsValid)
            {
                _responseDTO = await _facultyService.GetFacultyCommitteesDetails(FacultyID);
                if (_responseDTO.IsDone)
                {
                    return StatusCode(_responseDTO.StatusCode, _responseDTO.Model);
                }
                return StatusCode(_responseDTO.StatusCode, _responseDTO.Message);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("FacultyCommitteesForCurrentDay")]
        public async Task<IActionResult> GetFacultyCommitteesForCurrentDay(int FacultyID)
        {
            if (ModelState.IsValid)
            {
                _responseDTO = await _facultyService.GetFacultyCommitteesForCurrentDay(FacultyID);
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
