using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.Faculty
{
    public class FacultyDTO
    {
        public string? FacultyName { get; set; }

        public object? BYlaw { get; set; }

        public object? StudyMethod { get; set; }

        public object? facultyNode { get; set; }

        public object? facultyPhase { get; set; }
        public object? facultysemster { get; set; }
    }
    public class BylawDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class StudyMethodDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class facultyNodeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class facultyPhaseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class facultysemsterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

}
