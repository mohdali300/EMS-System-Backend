using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("FACULTY__NODES")]
public partial class FacultyNode
{
    [Key]
    [Column("FACULTY_NODE_ID")]
    public int FacultyNodeId { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("CODE")]
    [StringLength(50)]
    public string? Code { get; set; }

    [Column("ORDER")]
    public int? Order { get; set; }

    [Column("LEVEL")]
    public int? Level { get; set; }

    [Column("FACULTY_ID")]
    public int? FacultyId { get; set; }

    [Column("PARENT_ID")]
    public int? ParentId { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("FacultyNodes")]
    public virtual Faculty? Faculty { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<FacultyNode> InverseParent { get; set; } = new List<FacultyNode>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual FacultyNode? Parent { get; set; }

    [InverseProperty("FacultyNode")]
    public virtual ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();

    [InverseProperty("FacultyNode")]
    public virtual ICollection<Committee> Committees { get; set; }=new List<Committee>();
}
