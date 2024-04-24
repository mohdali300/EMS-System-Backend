using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class FacultyType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
