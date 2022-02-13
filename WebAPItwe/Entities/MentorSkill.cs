using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class MentorSkill
    {
        public string Id { get; set; }
        public string SkillId { get; set; }
        public string MentorId { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
