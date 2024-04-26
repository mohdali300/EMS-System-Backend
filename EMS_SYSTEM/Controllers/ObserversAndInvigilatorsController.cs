using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS_SYSTEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObserversAndInvigilatorsController : ControllerBase
    {
        private readonly IObserversAndInvigilatorsService _observersAndInvigilatorsService;
        public ObserversAndInvigilatorsController(IObserversAndInvigilatorsService observersAndInvigilatorsService)
        {
            _observersAndInvigilatorsService = observersAndInvigilatorsService;
        }
        [HttpGet("{id:int}",Name ="GetById")]

        public async Task<IActionResult> GetById(int id)
        {
          ObserversAndInvigilatorsDTO dto = await _observersAndInvigilatorsService.GetByID(id);
            if (dto != null)
            {
                return Ok(dto);
            }
            else
            {
                return NotFound($"No Observer Or Invigilator was found with ID{id}");
            }


        }

    }
}
