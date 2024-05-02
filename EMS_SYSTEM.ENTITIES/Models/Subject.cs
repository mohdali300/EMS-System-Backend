using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("SUBJECTS")]
public partial class Subject
{
    [Key]
    [Column("ID")]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("FACULTY_NODE_ID")]
    public int FacultyNodeId { get; set; }
    [Column("FACULTY_Hieryrical_ID")]
    public int FacultyHieryricalId { get; set; }

    [Column("MAX_DEGREE")]
    public int? MaxDegree { get; set; }

    [Column("MIN_DEGREE")]
    public int? MinDegree { get; set; }

    [Column("CREDIT_HOURS")]
    public int? CreditHours { get; set; }

    [ForeignKey("FacultyNodeId")]
    public virtual FacultyNode FacultyNode { get; set; }
    [ForeignKey("FacultyHieryricalId")]

    public virtual FacultyHieryical FacultyHieryrical { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<StudentSemesterSubject> StudentSemesterSubjects { get; set; } = new List<StudentSemesterSubject>();

    [InverseProperty("Subject")]
    public virtual ICollection<SubjectAssess> SubjectAssesses { get; set; } = new List<SubjectAssess>();

    [InverseProperty("Subject")]
    public ICollection<SubjectCommittee> SubjectCommittees { get; set; }=new List<SubjectCommittee>();
}
