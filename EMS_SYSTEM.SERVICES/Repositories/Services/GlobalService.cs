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

        public async Task<ResponseDTO> GetFacultiesByDate(DateTime date)
        {
            var faculties = await _context.Committees.Where(c => c.Date == date)
                .SelectMany(c => c.SubjectCommittees).Where(sc => sc.Subject != null)
                .Select(c => c.Subject).Where(s => s.FacultyNode != null)
                .Select(s => s.FacultyNode).Where(n => n.Faculty != null)
                .Select(n => n.Faculty.FacultyName).Distinct().ToListAsync();

            #region another query with FacultyHieryrical and semester
            //var faculties = await _context.Committees.Where(c => c.Date == date)
            //    .SelectMany(c => c.SubjectCommittees).Where(sc => sc.Subject != null)
            //    .Select(c => c.Subject).Where(s => s.FacultyHieryrical != null)
            //    .Select(s => s.FacultyHieryrical).Where(h => h.Semeter != null)
            //    .Select(h => h.Semeter).Where(sem => sem.Faculty != null)
            //    .Select(sem => sem.Faculty.FacultyName).Distinct().ToListAsync(); 
            #endregion

            if (faculties != null && faculties.Count>0)
            {
                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsDone = true,
                    Model = faculties
                };
            }

            return new ResponseDTO
            {
                StatusCode = 400,
                IsDone = false,
                Message="There is no faculties committees at this date."
            };
        }


    }
}
