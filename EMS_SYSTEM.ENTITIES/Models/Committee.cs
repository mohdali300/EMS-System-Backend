﻿using EMS_SYSTEM.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.DOMAIN.Models
{
    public partial class Committee
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public string Interval { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string SubjectName { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
        public Days Day { get; set; }
        public string StudyMethod { get; set; }  
        public string ByLaw { get; set; }
        public string FacultyNode { get; set; }
        public string FacultyPhase { get; set; }
        public virtual ICollection<SubjectCommittee> SubjectCommittees { get; set; } = new List<SubjectCommittee>();


    }
}
