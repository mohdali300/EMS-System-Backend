using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class Bylaw
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? FacultyId { get; set; }

    public int? CodeStudyMethodId { get; set; }

    public virtual StudeyMethod? CodeStudyMethod { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<FacultyHieryical> FacultyHieryicals { get; set; } = new List<FacultyHieryical>();
}
