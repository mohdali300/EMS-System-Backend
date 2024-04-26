﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Keyless]
[Table("STAFF")]
public partial class Staff
{
    [Column("ID")]
    public int? Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("ADDRESS")]
    [StringLength(50)]
    public string? Address { get; set; }

    [Column("EMAIL")]
    [StringLength(50)]
    public string? Email { get; set; }

    [Column("FACULTY_ID")]
    public int? FacultyId { get; set; }

    [ForeignKey("FacultyId")]
    public virtual Faculty? Faculty { get; set; }
}
