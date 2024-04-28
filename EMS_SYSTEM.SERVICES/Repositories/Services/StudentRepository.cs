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
    public class StudentRepository : GenericRepository<Student>,IStudentRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentRepository(UnvcenteralDataBaseContext Db):base(Db)
        {
        }

        public async Task<ResponseDTO> GetStudentDataByID(int Id)
        {
            var studentDTO = await _context.Students
            .Where(s => s.Id == Id)
            .SelectMany(student => student.StudentSemesters
            .Select(semester => new StudentDTO
            {
                Name = student.Name,
                FacultyCode = student.FacultyCode,
                Status = semester.StuentSatuts.StuentSatuts,
                Department = semester.FacultyNode.Name,
                Level = semester.FacultyHieryical.Phase.Name,
                FacultyName = semester.FacultyNode.Faculty.FacultyName
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
    }
}
