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
    public class ObserversAndInvigilatorsService:GenericRepository<Staff>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ObserversAndInvigilatorsService(UnvcenteralDataBaseContext Db, IUnitOfWork unitOfWork) : base(Db)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetByID(int id)
        {
            var staff=await _unitOfWork.Staff.GetByIDAsync(id);

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
