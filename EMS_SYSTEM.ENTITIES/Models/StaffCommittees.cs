using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.Models
{
    public class StaffCommittees
    {
        public int Id { get; set; }
        public int StaffID { get; set; }
        public int CommitteeID { get; set; }
        [ForeignKey("CommitteeID")]
        public virtual Committee? Committee { get; set; }
        [ForeignKey("StaffID")]
        public virtual Staff? Staff { get; set; }
    }
}
