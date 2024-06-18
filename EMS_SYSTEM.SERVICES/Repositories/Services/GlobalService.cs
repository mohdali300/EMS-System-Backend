using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class GlobalService:IGlobalService
    {
        private readonly UnvcenteralDataBaseContext _context;

        public GlobalService(UnvcenteralDataBaseContext context)
        {
            _context = context;
        }


    }
}
