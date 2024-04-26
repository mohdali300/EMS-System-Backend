using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("STUDENT_SEMESTER_SUBJECT")]
public partial class StudentSemesterSubject
{
    [Key]
    [Column("Student_Subject_Semter_Id")]
    public int StudentSubjectSemterId { get; set; }

    [Column("Subject_id")]
    public int? SubjectId { get; set; }

    [Column(TypeName = "decimal(8, 3)")]
    public decimal? Degree { get; set; }

    [Column("Is_Passed")]
    public int? IsPassed { get; set; }

    [Column("STUDENT_SEMESTERS_Id")]
    public int? StudentSemestersId { get; set; }

    [ForeignKey("StudentSemestersId")]
    [InverseProperty("StudentSemesterSubjects")]
    public virtual StudentSemester? StudentSemesters { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("StudentSemesterSubjects")]
    public virtual Subject? Subject { get; set; }
}
