using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitteeController : ControllerBase
    {
        private readonly ICommitteeService _committee;
        public CommitteeController(ICommitteeService _committee)
        {
            this._committee= _committee;
        }
        [HttpPost("AddCommittee")]
        public async Task<IActionResult> AddCommittee(CommitteeDTO model)
        {
            if (ModelState.IsValid)
            {
                var Response = await _committee.AddCommitteeAsync(model);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode , Response.Message);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetCommitteesForFaculty")]
        public async Task<IActionResult> GetCommitteesForFaculty(int FacultyID)
        {
            if (ModelState.IsValid)
            {
                var Response = await _committee.GetCommitteesForFaculty(FacultyID);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode, Response.Model);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
