using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class FacultyHieryical
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Order { get; set; }

    public int? ParentId { get; set; }

    public int? BylawId { get; set; }

    public int? SemeterId { get; set; }

    public int? PhaseId { get; set; }

    public virtual Bylaw? Bylaw { get; set; }

    public virtual FacultyPhase? Phase { get; set; }

    public virtual FacultySemester? Semeter { get; set; }

    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
}
