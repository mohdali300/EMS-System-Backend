using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("STUDENTS")]
public partial class Student
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("ADDRESS")]
    [StringLength(50)]
    public string? Address { get; set; }

    [Column("CITYID")]
    public int? Cityid { get; set; }

    [Column("NATIONALID")]
    [StringLength(50)]
    public string? Nationalid { get; set; }

    [Column("FACULTYCODE")]
    [StringLength(50)]
    public string? FacultyCode { get; set; } // STUDENT FACULTY CODE الكود الجامعي

    [Column("GENDER")]
    public int? Gender { get; set; }

    [Column("DATEOFBRITH")]
    public DateOnly? Dateofbrith { get; set; }

    [Column("FACULTYID")]
    public int? Facultyid { get; set; }

    [Column("MOBILE")]
    [StringLength(50)]
    public string? Mobile { get; set; }

    [Column("EMAIL")]
    [StringLength(50)]
    public string? Email { get; set; }

    [ForeignKey("Facultyid")]
    [InverseProperty("Students")]
    public virtual Faculty? Faculty { get; set; }

    [InverseProperty("Stuent")]
    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
    public virtual ICollection<Committee> Committees { get; set; } = new List<Committee>();
}
