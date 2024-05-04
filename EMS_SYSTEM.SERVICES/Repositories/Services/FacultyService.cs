using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net.Security;

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

            var departmentnames = await _context.FacultyNodes.Where(d => d.FacultyId == Id)
                .Select(d => new facultyNodeDTO { Id = d.FacultyNodeId, Name = d.Name })
                .ToListAsync()
                ;
            var facultyphases = await _context.FacultyPhases.Where(p => p.FacultyId == Id)
                .Select(p => new facultyPhaseDTO { Id = p.Id, Name = p.Name })
                .ToListAsync();


            var facultyBylaws = await _context.Bylaws
                                               .Where(l => l.FacultyId == Id)
                                               .Select(l => new BylawDTO { Id = l.Id, Name = l.Name })
                                               .ToListAsync();


            var studymethod = await _context.Bylaws
                .Where(s => s.FacultyId == Id).Select(s => new StudyMethodDTO { Id = (int)s.CodeStudyMethodId!, Name = s.CodeStudyMethod!.Name })
                .ToListAsync();

            var facultysemster = await _context.FacultySemesters
                .Where(f => f.FacultyId == Id).Select(f => new facultysemsterDTO { Id = f.Id, Name = f.Name })
                .ToListAsync();

            var faculty = new FacultyDTO
            {
                BYlaw = facultyBylaws,
                StudyMethod = studymethod,
                facultyNode = departmentnames,
                facultyPhase = facultyphases,
                facultysemster = facultysemster,

            };
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
                    Message = "Couldn't Find The Faculty"
                };
            }
        }

        public async Task<ResponseDTO> GetSubjects([FromBody]FacultyHieryicalDTO hieryicalDTO)
        {
            var Hierarchical = await _context.FacultyHieryicals.Include(h => h.Subjects)
                .Where
                (h =>
                h.BylawId ==hieryicalDTO.BylawId &&
                h.PhaseId == hieryicalDTO.PhaseId &&
                h.SemeterId == hieryicalDTO.FacultySemesterId
                ).FirstOrDefaultAsync();


            if (Hierarchical == null)
            {

                return new ResponseDTO
                {
                    StatusCode = 400,
                    IsDone = false,
                    Message = "Faculty hierarchical record not found"
                };
            }

            var Subjects = Hierarchical.Subjects
                .Where
                (s =>
                s.FacultyNodeId == hieryicalDTO.FacultyNodeId &&
                s.FacultyHieryricalId == Hierarchical.Id
                )
             .Select(s => new SubjectDTO
             {
                 Id = s.Id,
                 Name = s.Name

             }).ToList();




            return new ResponseDTO
            {
                Model = Subjects,
                StatusCode = 200,
                IsDone = true
            };
            
            
        }

    }
}
