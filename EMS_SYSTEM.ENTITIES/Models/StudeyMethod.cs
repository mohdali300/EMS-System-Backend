using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("STUDEY_METHOD")]
public partial class StudeyMethod
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("CodeStudyMethod")]
    public virtual ICollection<Bylaw> Bylaws { get; set; } = new List<Bylaw>();
}
