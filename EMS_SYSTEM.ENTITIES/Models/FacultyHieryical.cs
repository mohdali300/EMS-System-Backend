using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("FACULTY_HIERYICAL")]
public partial class FacultyHieryical
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("ORDER")]
    public int? Order { get; set; }

    [Column("PARENT_ID")]
    public int? ParentId { get; set; }

    [Column("BYLAW_ID")]
    public int? BylawId { get; set; }

    [Column("SEMETER_ID")]
    public int? SemeterId { get; set; }

    [Column("PHASE_ID")]
    public int? PhaseId { get; set; }

    [ForeignKey("BylawId")]
    [InverseProperty("FacultyHieryicals")]
    public virtual Bylaw? Bylaw { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<FacultyHieryical> InverseParent { get; set; } = new List<FacultyHieryical>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual FacultyHieryical? Parent { get; set; }

    [ForeignKey("PhaseId")]
    [InverseProperty("FacultyHieryicals")]
    public virtual FacultyPhase? Phase { get; set; }

    [ForeignKey("SemeterId")]
    [InverseProperty("FacultyHieryicals")]
    public virtual FacultySemester? Semeter { get; set; }

    [InverseProperty("FacultyHieryical")]
    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
}
