using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ResponseDTO> GetAllFaculties()
        {
            var Faculties = await _context.Faculties
                               .AsNoTracking()
                               .ToListAsync();
            if (Faculties.Any())
            {
                return new ResponseDTO {
                    StatusCode = 200,
                    IsDone = true, 
                    Model = Faculties.Select(s=>s.FacultyName) 
                };
            }
            return new ResponseDTO { 
                StatusCode = 400 , 
                IsDone=false
            };
        }
        public async Task<ResponseDTO> GetFacultyByName(string FacultyName)
        {
            var Faculty = await _context.Faculties
                .FirstOrDefaultAsync(s => s.FacultyName.Contains(FacultyName));
                
            if (Faculty is not null)
            {
                return new ResponseDTO {
                    StatusCode = 200,
                    IsDone = true, 
                    Model = Faculty.FacultyName 
                };
            }
            return new ResponseDTO { 
                Message = "Faculty Not Found in System",
                IsDone = false,
                StatusCode = 400 
            };
        }

    }
}
