using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("BYLAW")]
public partial class Bylaw
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("FACULTY_ID")]
    public int? FacultyId { get; set; }

    [Column("CODE_STUDY_METHOD_ID")]
    public int? CodeStudyMethodId { get; set; }

    [ForeignKey("CodeStudyMethodId")]
    [InverseProperty("Bylaws")]
    public virtual StudeyMethod? CodeStudyMethod { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("Bylaws")]
    public virtual Faculty? Faculty { get; set; }

    [InverseProperty("Bylaw")]
    public virtual ICollection<FacultyHieryical> FacultyHieryicals { get; set; } = new List<FacultyHieryical>();
    public virtual ICollection<Committee> Committees { get; set; } = new List<Committee>();
}
