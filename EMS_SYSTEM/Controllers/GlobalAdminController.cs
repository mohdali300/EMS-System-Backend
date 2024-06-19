using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalAdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GlobalAdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetFacultiesByDate(DateTime date)
        {
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.Global.GetFacultiesByDate(date);
                if (response.IsDone)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
