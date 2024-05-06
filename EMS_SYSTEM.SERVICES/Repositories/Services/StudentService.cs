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
                Status = semester.StuentSatuts.StuentSatuts,
                Department = semester.FacultyNode.Name,
                Level = semester.FacultyHieryical.Phase.Name,
                FacultyName = semester.FacultyNode.Faculty.FacultyName,
                FacultyId = semester.FacultyNode.Faculty.Id
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
       .Where(ss => ss.FacultyHieryical != null && ss.FacultyHieryical.Phase != null && ss.FacultyHieryical.Phase.Id == phaseid && ss.FacultyNode!.FacultyNodeId == nodeid && ss.Id == semsterid)
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
                var otherStudentsInSubject = await _context.Students
                    .Where(s => s.StudentSemesters.Any(ss => ss.StudentSemesterSubjects.Any(sss => sss.SubjectId == subjectId)))
                    .Where(s => s.Nationalid != studentNationalId)
                    .OrderBy(s => s.Name)
                    .ToListAsync();

                var studentIndex = otherStudentsInSubject.FindIndex(s => s.Nationalid == studentNationalId);
                studentOrder.Add(subjectId, studentIndex != -1 ? studentIndex + 1 : 0);
            }

            return studentOrder;
        }

        public async Task<ResponseDTO> GetStudentCommitteesBySubject(string studentNationalId)
        {
            try
            {
                var studentId = await _context.Students
                    .Where(s => s.Nationalid == studentNationalId).Select(s=>s.Id).FirstOrDefaultAsync();
                var studentCommittees = await _context.StudentsCommittees
                    .Where(sc => sc.StudentID == studentId)
                    .Join(_context.Committees, sc => sc.CommitteeID, c => c.Id, (sc, c) => new
                    {
                        StudentName = sc.Student.Name,
                        SubjectName = sc.Committee.SubjectName,
                        CommitteeName = sc.Committee.Name,
                        CommitteeDate = sc.Committee.Date,
                        committeeinterval = sc.Committee.Interval,
                        Committeeplace = sc.Committee.Place,
                        Committeeday = sc.Committee.Day,
                        Committeefrom = sc.Committee.From,
                        Committeeto = sc.Committee.To,


                    }).ToListAsync();




                return new ResponseDTO
                {
                    StatusCode = 200,
                    IsDone = true,
                    Model = studentCommittees
                };
                
                
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    StatusCode = 500,
                    IsDone = false,
                    Message = ex.Message,
                    Status = "Internal Server Error"
                };
            }
        }

    }
}
