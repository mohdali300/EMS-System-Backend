using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("ASSESS")]
public partial class Assess
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("Assess")]
    public virtual ICollection<SubjectAssess> SubjectAssesses { get; set; } = new List<SubjectAssess>();
}
