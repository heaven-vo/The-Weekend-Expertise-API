using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Skill")]
    public partial class Skill
    {
        public Skill()
        {
            MajorSkills = new HashSet<MajorSkill>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Boolean Status { get; set; }

        public virtual ICollection<MajorSkill> MajorSkills { get; set; }
    }
}
