﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Subject")]
    public partial class Subject
    {
        public Subject()
        {
            SkillSubjects = new HashSet<SkillSubject>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string MajorId { get; set; }
        public string Image { get; set; }
        public Boolean Status { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<SkillSubject> SkillSubjects { get; set; }
    }
}
