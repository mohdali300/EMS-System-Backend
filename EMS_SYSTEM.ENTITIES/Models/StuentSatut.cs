using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("STUENT_SATUTS")]
public partial class StuentSatut
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("STUENT_SATUTS")]
    [StringLength(50)]
    public string? StuentSatuts { get; set; }

    [InverseProperty("StuentSatuts")]
    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
}
