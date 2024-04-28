using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class ObserversAndInvigilatorsService:GenericRepository<Staff>, IObserversAndInvigilatorsService
    {

        public ObserversAndInvigilatorsService(UnvcenteralDataBaseContext Db):base(Db)
        {
        }

        public async Task<ResponseDTO> GetByID(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff != null)
            {
                return new ResponseDTO
                {
                    Model = staff.Name,
                    StatusCode = 200,
                    IsDone = true,
                };
            }
            return new ResponseDTO
            {
                StatusCode = 400,
                IsDone = false,
                Message="Sorry, This staff is not exist!"
            };
        }

    }
}
