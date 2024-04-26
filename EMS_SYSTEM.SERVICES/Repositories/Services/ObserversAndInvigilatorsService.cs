using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class ObserversAndInvigilatorsService : IObserversAndInvigilatorsService
    {
        private readonly UnvcenteralDataBaseContext _context;
        public ObserversAndInvigilatorsService(UnvcenteralDataBaseContext context)
        {
            this._context = context;
        }
        public async Task<ObserversAndInvigilatorsDTO>  GetByID(int id)
        {
            Staff staff = _context.Staff.FirstOrDefault(s => s.Id == id);

            if (staff != null)
            {

                return new ObserversAndInvigilatorsDTO { Name = staff.Name};
            }
            else return null;
            
        }

    }
}
