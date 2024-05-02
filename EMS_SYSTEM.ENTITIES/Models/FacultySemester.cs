using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("FACULTY_SEMESTER")]
public partial class FacultySemester
{
    [Key]
    [Column("ID")]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("CODE")]
    [StringLength(50)]
    public string? Code { get; set; }

    [Column("FACULTY_ID")]
    public int? FacultyId { get; set; }

    [Column("ORDER")]
    public int? Order { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("FacultySemesters")]
    public virtual Faculty? Faculty { get; set; }

    [InverseProperty("Semeter")]
    public virtual ICollection<FacultyHieryical> FacultyHieryicals { get; set; } = new List<FacultyHieryical>();

}
