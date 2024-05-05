using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.APPLICATION.Repositories.Services.UnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObserversAndInvigilatorsController : ControllerBase
    {
        private readonly ObserversAndInvigilatorsService _observers;
        private ResponseDTO response;

        public ObserversAndInvigilatorsController(ObserversAndInvigilatorsService observers)
        {
            _observers = observers;
            response = new();
        }

        [HttpGet("{id}", Name ="GetById")]
        [Authorize(Roles = "Observers , Invigilators , FacultyAdmin ,GlobalAdmin")]
        public async Task<IActionResult> GetById(string id)
        {
            if(ModelState.IsValid)
            {
                response = await _observers.GetByNID(id);

                if(response.IsDone)
                {
                    return Ok(response);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("StaffCommittee")]
        public async Task<IActionResult> GetCommittees(string nid)
        {
            if(ModelState.IsValid)
            {
                response=await _observers.GetStaffCommittees(nid);
                if (response.IsDone)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode,response.Message);
            }
            return BadRequest(ModelState);
        }

    }
}
