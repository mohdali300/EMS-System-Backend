using EMS_SYSTEM.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.DTO.Committee
{
    public  class CommitteeDTO
    {
        public string Name { get; set; }
        public int BylawID { get; set; }
        public int StudyMethodID { get; set; }
        public int FacultyNodeID { get; set; }
        public int FacultyPhaseID { get; set; }
        public int SubjectID  { get; set; }
        public Days Day { get; set; }
        public DateOnly Date { get; set; }
        public string Interval { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
    }
}
