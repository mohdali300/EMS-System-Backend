using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.DOMAIN.DTO.Student;
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

        public StudentDTO GetData(string Id)
        {
            return new StudentDTO { };
        }
    }
}
