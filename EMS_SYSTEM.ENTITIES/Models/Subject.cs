using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? FacultyHieryicalId { get; set; }

    public int? MaxDegree { get; set; }

    public int? MinDegree { get; set; }

    public int? FacultySemesterId { get; set; }

    public int? FacultyPhasesId { get; set; }

    public int? CreditHours { get; set; }

    public virtual FacultyPhase? FacultyPhases { get; set; }

    public virtual FacultySemester IdNavigation { get; set; } = null!;

    public virtual ICollection<SubjectAssess> SubjectAssesses { get; set; } = new List<SubjectAssess>();
}
