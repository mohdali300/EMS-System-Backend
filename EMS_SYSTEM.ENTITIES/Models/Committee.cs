using EMS_SYSTEM.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.Models
{
    public partial class Committee
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public string Interval { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
        public Days Day {  get; set; }

        
        public int ByLawId { get; set; }
        public int FacultyNodeId { get; set; }
        public int FacultyPhaseId { get; set; }

        [ForeignKey("ByLawId")]
        public virtual Bylaw Bylaw { get; set; }

        [ForeignKey("FacultyPhaseId")]
        public virtual FacultyPhase? FacultyPhase { get; set; }

        [ForeignKey("FacultyNodeId")]
        public virtual FacultyNode FacultyNode { get; set; }
        public ICollection<SubjectCommittee> SubjectCommittees { get; set; }=new List<SubjectCommittee>();


    }
}
