using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class StudentSemester
{
    public int Id { get; set; }

    public int? StuentId { get; set; }

    public int? FacultyHieryicalId { get; set; }

    public int? AcadYearId { get; set; }

    public decimal? Gpa { get; set; }

    public decimal? Precentage { get; set; }

    public decimal? Toal { get; set; }

    public int? IsPass { get; set; }

    public int? StuentSatutsId { get; set; }

    public int? FacultyNodeId { get; set; }

    public virtual AcadYead? AcadYear { get; set; }

    public virtual FacultyHieryical? FacultyHieryical { get; set; }

    public virtual FacultyNode? FacultyNode { get; set; }

    public virtual Student? Stuent { get; set; }
}
