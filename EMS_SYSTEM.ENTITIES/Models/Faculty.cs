using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class Faculty
{
    public int Id { get; set; }

    public string? FacultyName { get; set; }

    public string? FacultyCode { get; set; }

    public string? FacultyAddress { get; set; }

    public int? FacultyTypeId { get; set; }

    public virtual ICollection<Bylaw> Bylaws { get; set; } = new List<Bylaw>();

    public virtual ICollection<FacultyNode> FacultyNodes { get; set; } = new List<FacultyNode>();

    public virtual ICollection<FacultyPhase> FacultyPhases { get; set; } = new List<FacultyPhase>();

    public virtual ICollection<FacultySemester> FacultySemesters { get; set; } = new List<FacultySemester>();

    public virtual FacultyType? FacultyType { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
