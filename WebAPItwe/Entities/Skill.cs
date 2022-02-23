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
            MentorSkills = new HashSet<MentorSkill>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MentorSkill> MentorSkills { get; set; }
    }
}
