using EMS_SYSTEM.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.Committee
{
    public class CommitteeDTO
    {
        public string Name { get; set; }
        public string StudyMethod { get; set; }
        public string ByLaw { get; set; }
        public string FacultyNode { get; set; }
        public string FacultyPhase { get; set; }
        public string SubjectsName  { get; set; }
        public Days Day { get; set; }
        public DateTime Date { get; set; }
        public string Interval { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
        public int SubjectID { get; set; }

    }
}
