using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
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
    public class StudentService : IStudentService
    {
        private readonly UnvcenteralDataBaseContext _Db;
        public StudentService(UnvcenteralDataBaseContext _Db)
        {
            this._Db = _Db;
        }


        public async Task<StudentDTO> GetStudentByID(int Id)
        {
            var studentDTO = await _Db.Students
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

            return studentDTO;

        }
    }
}
