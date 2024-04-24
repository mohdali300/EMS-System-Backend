using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class AcadYead
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
}
