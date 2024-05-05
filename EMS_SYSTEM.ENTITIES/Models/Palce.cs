using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("PALCES")]
public partial class Palce
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ADDRESS")]
    [StringLength(50)]
    public string? Address { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("CAPACITY")]
    public int? Capacity { get; set; }

    public virtual ICollection<Committee> Committees { get; set; }
}
