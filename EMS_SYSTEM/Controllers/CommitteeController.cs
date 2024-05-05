using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using Microsoft.AspNetCore.Authorization;
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
      //  [Authorize(Roles = "FacultyAdmin , GlobalAdmin")]
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


        [HttpGet("Schedule")]
        public async Task<IActionResult> GetLevelSchedule(int Id)
        {
            if (ModelState.IsValid)
            {
                var _responseDTO = await _committee.GetCommitteesSchedule(Id);
                if (_responseDTO.IsDone)
                {
                    return StatusCode(_responseDTO.StatusCode, _responseDTO.Model);
                }
                return StatusCode(_responseDTO.StatusCode, _responseDTO.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("FilteringForCommittees")]
        public async Task<IActionResult> FilterFacultyCommittees(int FacultyID, int Level=0, string CommitteeName=null , string subjectName = null)
        {
            if (ModelState.IsValid)
            {
                var _responseDTO = await _committee.FilterFacultyCommittees(FacultyID,Level,CommitteeName,subjectName);
                if (_responseDTO.IsDone)
                {
                    return StatusCode(_responseDTO.StatusCode, _responseDTO.Model);
                }
                return StatusCode(_responseDTO.StatusCode, _responseDTO.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteCommittee")]
        public async Task<IActionResult> DeleteCommittee(int CommitteeId)
        {
            if (ModelState.IsValid)
            {
                var Response = await _committee.DeleteCommittee(CommitteeId);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode, Response.Model);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("DeleteAllFacultyCommittee")]
        public async Task<IActionResult> DeleteAllFacultyCommittee(int FacultyID)
        {
            if (ModelState.IsValid)
            {
                var Response = await _committee.DeleteAllFacultyCommitee(FacultyID);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode, Response.Model);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateCommitee")]
        public async Task<IActionResult> UpdateCommitee(int committeeID, CommitteeDTO model)
        {
            if (ModelState.IsValid)
            {
                var Response = await _committee.UpdateCommitee(committeeID, model);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode, Response.Model);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }
        //[HttpGet("GetfacultyObservers")]
        //public async Task<IActionResult> GetObserversInFaculty(int facultyId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _committee.GetObserversInFaculty(facultyId);
        //        if (response.Any())
        //        {
        //            var responseDTOs = response.Select(res => new ResponseDTO
        //            {
        //                Model = res,
        //                StatusCode = 200,
        //                IsDone = true
        //            }).ToList();

        //            return Ok(responseDTOs);
        //        }
        //        else
        //        {
        //            var errorResponse = new ResponseDTO
        //            {
        //                StatusCode = 404,
        //                IsDone = false,
        //                Message = "No observers found for the specified faculty."
        //            };

        //            return NotFound(errorResponse);
        //        }
        //    }
        //    return BadRequest(ModelState);
        //}
        [HttpGet("GetfacultyObservers")]
        public async Task<IActionResult> GetObserversInFaculty(int facultyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _committee.GetObserversInFaculty(facultyId);
                return StatusCode(response.StatusCode, response.Model);
            }

            return BadRequest(ModelState);
        }
        [HttpGet("GetfacultyInvigilators")]
        public async Task<IActionResult> GetInvigilatorsInFaculty(int facultyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _committee.GetInvigilatorsInFaculty(facultyId);
                return StatusCode(response.StatusCode, response.Model);
            }

            return BadRequest(ModelState);
        }


    }
}
