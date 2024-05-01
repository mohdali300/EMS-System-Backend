using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.APPLICATION.Repositories.Services.UnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObserversAndInvigilatorsController : ControllerBase
    {
        private readonly ObserversAndInvigilatorsService _observers;
        public ObserversAndInvigilatorsController(ObserversAndInvigilatorsService observers)
        {
            _observers = observers;
        }

        [HttpGet("{id:int}",Name ="GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            if(ModelState.IsValid)
            {
                ResponseDTO response = new();
                response = await _observers.GetByNID(id);

                if(response != null)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

    }
}
