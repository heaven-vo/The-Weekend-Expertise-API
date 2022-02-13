using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
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
