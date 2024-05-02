using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("STUDENT_SEMESTERS")]
public partial class StudentSemester
{
    [Key]
    [Column("ID")]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("STUENT_ID")]
    public int? StuentId { get; set; }

    [Column("FACULTY_HIERYICAL_ID")]
    public int? FacultyHieryicalId { get; set; }

    [Column("ACAD_YEAR_ID")]
    public int? AcadYearId { get; set; }

    [Column("GPA", TypeName = "decimal(5, 3)")]
    public decimal? Gpa { get; set; }

    [Column("PRECENTAGE", TypeName = "decimal(5, 3)")]
    public decimal? Precentage { get; set; }

    [Column("TOAL", TypeName = "decimal(8, 3)")]
    public decimal? Toal { get; set; }

    [Column("IS_PASS")]
    public int? IsPass { get; set; }

    [Column("STUENT_SATUTS_ID")]
    public int? StuentSatutsId { get; set; }

    [Column("FACULTY_NODE_ID")]
    public int? FacultyNodeId { get; set; }

    [ForeignKey("AcadYearId")]
    [InverseProperty("StudentSemesters")]
    public virtual AcadYead? AcadYear { get; set; }

    [ForeignKey("FacultyHieryicalId")]
    [InverseProperty("StudentSemesters")]
    public virtual FacultyHieryical? FacultyHieryical { get; set; }

    [ForeignKey("FacultyNodeId")]
    [InverseProperty("StudentSemesters")]
    public virtual FacultyNode? FacultyNode { get; set; }

    [InverseProperty("StudentSemesters")]
    public virtual ICollection<StudentSemesterSubject> StudentSemesterSubjects { get; set; } = new List<StudentSemesterSubject>();

    [ForeignKey("StuentId")]
    [InverseProperty("StudentSemesters")]
    public virtual Student? Stuent { get; set; }

    [ForeignKey("StuentSatutsId")]
    [InverseProperty("StudentSemesters")]
    public virtual StuentSatut? StuentSatuts { get; set; }
}
