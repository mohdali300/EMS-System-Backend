using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;
using Microsoft.EntityFrameworkCore;
using EMS_SYSTEM.DOMAIN.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class GlobalService: GenericRepository<Faculty>, IGlobalService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GlobalService(UnvcenteralDataBaseContext _context, IUnitOfWork _unitOfWork):base(_context)
        {
           this._unitOfWork= _unitOfWork;
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


        public async Task<ResponseDTO> GetFacultiesWithCommitteeCount()
        {
            var faculties = await _context.Faculties
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
                Model = faculties,
                StatusCode = 200,
                IsDone = true
            };
        }
        public async Task<ResponseDTO> GetAllFaculties()
        {
            var Faculties = await _unitOfWork.Faculty.GetAllAsync();
                              
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
            var Faculty = await _unitOfWork.Faculty.GetByNameAsync(c => c.FacultyName.Contains(FacultyName));
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

        public async Task<ResponseDTO> GetFacultiesWithCommitteeToday()
        {
            // Return faculites that contain committees only 
            var faculties = await _context.Committees
    .Where(c => c.Date.Date == DateTime.Today)
    .SelectMany(c => c.SubjectCommittees)
    .Where(sc => sc.Subject != null && sc.Subject.FacultyNode != null && sc.Subject.FacultyNode.Faculty != null)
    .GroupBy(sc => sc.Subject.FacultyNode.Faculty.FacultyName)
    .Select(g => new
    {
       
        FacultyName = g.Key,
        CommitteeCount = g.Count()
    }).OrderByDescending(g => g.CommitteeCount)
    .ToListAsync();

            /* Return All Faculties whether it contains committees or not
             var faculties = await _context.Faculties
    .GroupJoin(
        _context.Committees.Where(c => c.Date.Date == DateTime.Today)
            .SelectMany(c => c.SubjectCommittees)
            .Where(sc => sc.Subject != null && sc.Subject.FacultyNode != null),
        f => f.Id,
        sc => sc.Subject.FacultyNode.FacultyId,
        (f, subjectCommittees) => new
        {
            FacultyID = f.Id,
            FacultyName = f.FacultyName,
            CommitteeCount = subjectCommittees.Count()
        }).OrderByDescending(g => g.CommitteeCount )
    .ToListAsync();
        */



            if (faculties != null && faculties.Count > 0)
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
                Message = "There is no faculties committees Today."
            };
        }

    }
    }
   

