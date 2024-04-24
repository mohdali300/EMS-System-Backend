using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class StudeyMethod
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Bylaw> Bylaws { get; set; } = new List<Bylaw>();
}
