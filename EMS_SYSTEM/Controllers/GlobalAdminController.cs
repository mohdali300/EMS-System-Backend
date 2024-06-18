using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalAdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _responseDTO;

        public GlobalAdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDTO = new ResponseDTO();


       }
        [HttpGet("CommitteesCount")]


        public async Task<IActionResult> GetFacultiesWithCommitteeCount()
        {
            if (ModelState.IsValid)
            {
                _responseDTO = await _unitOfWork.Global.GetFacultiesWithCommitteeCount();
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
