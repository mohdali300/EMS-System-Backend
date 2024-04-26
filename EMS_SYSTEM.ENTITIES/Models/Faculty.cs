using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("FACULTY")]
public partial class Faculty
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("FACULTY_NAME")]
    [StringLength(100)]
    public string? FacultyName { get; set; }

    [Column("FACULTY_CODE")]
    [StringLength(100)]
    public string? FacultyCode { get; set; }

    [Column("FACULTY_ADDRESS")]
    [StringLength(100)]
    public string? FacultyAddress { get; set; }

    [Column("FACULTY_TYPE_ID")]
    public int? FacultyTypeId { get; set; }

    [InverseProperty("Faculty")]
    public virtual ICollection<Bylaw> Bylaws { get; set; } = new List<Bylaw>();

    [InverseProperty("Faculty")]
    public virtual ICollection<FacultyNode> FacultyNodes { get; set; } = new List<FacultyNode>();

    [InverseProperty("Faculty")]
    public virtual ICollection<FacultyPhase> FacultyPhases { get; set; } = new List<FacultyPhase>();

    [InverseProperty("Faculty")]
    public virtual ICollection<FacultySemester> FacultySemesters { get; set; } = new List<FacultySemester>();

    [ForeignKey("FacultyTypeId")]
    [InverseProperty("Faculties")]
    public virtual FacultyType? FacultyType { get; set; }

    [InverseProperty("Faculty")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
