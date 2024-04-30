using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.Models
{
    public partial class SubjectCommittee
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }
        public int CommitteeId { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("SubjectCommittee")]
        public virtual Subject? Subject { get; set; }

        [ForeignKey("CommitteeId")]
        [InverseProperty("SubjectCommittee")]
        public virtual Committee? Committee { get; set; }
    }
}
