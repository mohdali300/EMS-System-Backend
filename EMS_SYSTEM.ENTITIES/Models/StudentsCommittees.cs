using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.Models
{
    public class StudentsCommittees
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public int CommitteeID { get; set; }
        [ForeignKey("CommitteeID")]
        public virtual Committee? Committee { get; set; }
        [ForeignKey("StudentID")]

        public virtual Student? Student { get; set; }
    }
}
