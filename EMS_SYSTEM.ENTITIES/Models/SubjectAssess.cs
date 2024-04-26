using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("SUBJECT_ASSESS")]
public partial class SubjectAssess
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MAX_DEGREE")]
    public int? MaxDegree { get; set; }

    [Column("MIN_DEGREE")]
    public int? MinDegree { get; set; }

    [Column("SUBJECT_ID")]
    public int? SubjectId { get; set; }

    [Column("ASSESS_ID")]
    public int? AssessId { get; set; }

    [ForeignKey("AssessId")]
    [InverseProperty("SubjectAssesses")]
    public virtual Assess? Assess { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("SubjectAssesses")]
    public virtual Subject? Subject { get; set; }
}
