using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;
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

        public async Task<ResponseDTO> GetFacultiesWithCommitteeCount()
        {
            var result = await _context.Faculties
     .Select(f => new FacultyCommitteeCountDto
     {
         FacultyName = f.FacultyName,
         CommitteeCount = f.FacultyNodes
             .SelectMany(fn => fn.Subjects)
             .SelectMany(s => s.SubjectCommittees)
             .Count()
     })
     .OrderByDescending(r => r.CommitteeCount)
     .ToListAsync();

            return new ResponseDTO
            {
                Model = result,
                StatusCode = 200,
                IsDone = true
            };

        }
    }
}
