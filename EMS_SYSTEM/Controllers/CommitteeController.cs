﻿using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
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
        [Authorize(Roles = "FacultyAdmin , GlobalAdmin")]
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
        [HttpDelete("DeleteCommitee")]
        public async Task<IActionResult> DeleteCommitee(int CommiteeId)
        {
            if (ModelState.IsValid)
            {
                var Response = await _committee.DeleteCommitee(CommiteeId);
                if (Response.IsDone)
                {
                    return StatusCode(Response.StatusCode, Response.Model);
                }
                return StatusCode(Response.StatusCode, Response.Message);
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("DeleteAllFacultyCommitee")]
        public async Task<IActionResult> DeleteAllFacultyCommitee(int FacultyID)
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

    }
}
