using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class ObserversAndInvigilatorsService:GenericRepository<Staff>,IObserversAndInvigilatorsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ObserversAndInvigilatorsService(UnvcenteralDataBaseContext Db, IUnitOfWork unitOfWork) : base(Db)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetByNID(string id)
        {
            var staff=await _context.Staff.FirstOrDefaultAsync(s=>s.NID == id);

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
