using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class FacultyNode
{
    public int FacultyNodeId { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int? Order { get; set; }

    public int? Level { get; set; }

    public int? FacultyId { get; set; }

    public int? ParentId { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<FacultyNode> InverseParent { get; set; } = new List<FacultyNode>();

    public virtual FacultyNode? Parent { get; set; }

    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
}
