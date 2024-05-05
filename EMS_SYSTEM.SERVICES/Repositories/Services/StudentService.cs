using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Student;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class StudentService : GenericRepository<Student>,IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(UnvcenteralDataBaseContext Db):base(Db)
        {
        }

     

        public async Task<ResponseDTO> GetStudentDataByNID(string Id)
        {
            var studentDTO = await _context.Students
            .Where(s => s.Nationalid == Id)
            .SelectMany(student => student.StudentSemesters
            .Select(semester => new StudentDTO
            {
                Name = student.Name,
                FacultyCode = student.FacultyCode,
                //Status = semester.StuentSatuts.StuentSatuts,
                //Department = semester.FacultyNode.Name,
                //Level = semester.FacultyHieryical.Phase.Name,
                //FacultyName = semester.FacultyNode.Faculty.FacultyName,
                //FacultyId = semester.FacultyNode.Faculty.Id
            }))
            .FirstOrDefaultAsync();

            if(studentDTO != null)
            {
                return new ResponseDTO
                {
                    Model = studentDTO,
                    StatusCode = 200,
                    IsDone = true
                };
            }
            else
            {
                return new ResponseDTO
                {
                    StatusCode=400,
                    IsDone = false,
                    Message="Sorry, This Student is not exists!"
                };
            }

        }
        // method return list of ids of subject in which student join 
        public async Task<List<int>> GetSubjectsForStudent(string studentNationalId, int nodeid, int phaseid, int semsterid)
        {
            var subjectIds = await _context.Students
       .Where(s => s.Nationalid == studentNationalId)
       .SelectMany(s => s.StudentSemesters)
       .Where( ss => ss.FacultyHieryical != null && ss.FacultyHieryical.Phase != null && ss.FacultyHieryical.Phase.Id == phaseid && ss.FacultyNode!.FacultyNodeId == nodeid && ss.Id == semsterid)
       .SelectMany(ss => ss.StudentSemesterSubjects.Select(sss => sss.SubjectId))
       .ToListAsync();


            return subjectIds.Where(id => id.HasValue).Select(id => id!.Value).ToList();


        }

        // method return the order of the student in each subject in alphabetic order
        public async Task<Dictionary<int, int>> GetStudentOrderAmongOthers(string studentNationalId, int nodeid, int phaseid, int semsterid)
        {
            var studentOrder = new Dictionary<int, int>();
            var subjects = await GetSubjectsForStudent(studentNationalId, nodeid, phaseid, semsterid);

            foreach (var subjectId in subjects)
            {
                var order = await _context.Students
                    .Where(s => s.Nationalid != studentNationalId) 
                    .Where(s => s.StudentSemesters.Any(ss => ss.StudentSemesterSubjects.Any(sss => sss.SubjectId == subjectId))) // Filter by subject ID
                    .OrderBy(s => s.Name) // Order by student name 
                    .Select((s, index) => new { s.Name, Index = index + 1 }) 
                    .FirstOrDefaultAsync(s => s.Name == studentNationalId);

                studentOrder.Add(subjectId, order?.Index ?? 0);
            }

            return studentOrder;
        }

    }
}
