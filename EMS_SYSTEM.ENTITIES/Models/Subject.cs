using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("SUBJECTS")]
public partial class Subject
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("FACULTY_HIERYICAL_ID")]
    public int? FacultyHieryicalId { get; set; }

    [Column("MAX_DEGREE")]
    public int? MaxDegree { get; set; }

    [Column("MIN_DEGREE")]
    public int? MinDegree { get; set; }

    [Column("CREDIT_HOURS")]
    public int? CreditHours { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Subject")]
    public virtual FacultySemester IdNavigation { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<StudentSemesterSubject> StudentSemesterSubjects { get; set; } = new List<StudentSemesterSubject>();

    [InverseProperty("Subject")]
    public virtual ICollection<SubjectAssess> SubjectAssesses { get; set; } = new List<SubjectAssess>();
}
