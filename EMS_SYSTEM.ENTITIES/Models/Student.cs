using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? Cityid { get; set; }

    public string? Nationalid { get; set; }

    public int? Gender { get; set; }

    public DateOnly? Dateofbrith { get; set; }

    public int? Facultyid { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
}
