using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class FacultyPhase
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int? FacultyId { get; set; }

    public int? Order { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<FacultyHieryical> FacultyHieryicals { get; set; } = new List<FacultyHieryical>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
