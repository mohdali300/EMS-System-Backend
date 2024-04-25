using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.Student
{
    public class StudentDTO
    {
        public string? Name { get; set; }
        public int? Facultyid { get; set; }

        public string? FacultyName { get; set; }

        public string? Level { get; set; }
        public string? Status { get; set; }
        public string? Department {  get; set; }


    }
}
