using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;
using EMS_SYSTEM.DOMAIN.DTO.Student;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class FacultyService : GenericRepository<Faculty>, IFacultyService
    {

        private readonly IUnitOfWork _unitOfWork;
        public FacultyService(UnvcenteralDataBaseContext Db, IUnitOfWork unitOfWork) : base(Db)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetFacultyDataByID(int Id)
        {
            var departmentnames = _context.FacultyNodes.Where(d => d.FacultyId == Id).Select(d => d.Name).ToList();
            var facultyphases = _context.FacultyPhases.Where(p => p.FacultyId == Id).Select(p => p.Name).ToList();
            var facultybylaws=_context.Bylaws.Where(l=>l.FacultyId==Id).Select(l=>l.Name).ToList();
            var faculty = await _context.Faculties
                .Where(f => f.Id == Id).SelectMany
                (faculty => faculty.Bylaws.Select(Bylaw => new FacultyDTO

                {
                    FacultyName = faculty.FacultyName,
                    BYlaw = facultybylaws!,
                    StudyMethod = Bylaw.CodeStudyMethod!.Name,
                    facultyNode = departmentnames!,
                    facultyPhase = facultyphases!,

                }
                )).FirstOrDefaultAsync();
            if (faculty != null)
            {
                return new ResponseDTO
                {
                    Model = faculty,
                    StatusCode = 200,
                    IsDone = true
                };
            }
            else
            {
                return new ResponseDTO
                {
                    StatusCode = 400,
                    IsDone = false,
                    Message = "We Couldn't Find The Faculty"
                };
            }
        }
        //public async Task<ResponseDTO> GetSubjects(int bylawId, int facultyPhaseId, int facultyNodeId, int facultyId)
        //{
        //    var Hierarchical = await _context.FacultyHieryicals.FirstOrDefaultAsync
        //        (h =>
        //        h.BylawId == bylawId &&
        //        h.PhaseId == facultyPhaseId &&
        //        h.Bylaw.FacultyId == facultyId &&
        //        h.StudentSemesters.Any(s => s.FacultyNodeId == facultyNodeId));

        //    if (Hierarchical == null)
        //    {
        //        return new ResponseDTO
        //        {
        //            StatusCode = 400,
        //            IsDone = false,
        //            Message = "Faculty hierarchical record not found"
        //        };

        //    }

        // //   var subjects = await _context.Subjects
        //     //   .Where(s => s.FacultySemester.FacultyHieryicals.Contains(Hierarchical))
        //        //.ToListAsync();

        //    if (subjects != null)
        //    {
        //        return new ResponseDTO
        //        {
        //            Model = subjects,
        //            StatusCode = 200,
        //            IsDone = true
        //        };
        //    }
        //    else
        //    {
        //        return new ResponseDTO
        //        {
        //            StatusCode = 400,
        //            IsDone = false,
        //            Message = "No subjects found "
        //        };
        //    }
        //}
    }
}
