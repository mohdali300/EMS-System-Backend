using System;
using System.Collections.Generic;

namespace EMS_SYSTEM.DOMAIN.Models;

public partial class SubjectAssess
{
    public int Id { get; set; }

    public int? MaxDegree { get; set; }

    public int? MinDegree { get; set; }

    public int? SubjectId { get; set; }

    public virtual Subject? Subject { get; set; }
}
