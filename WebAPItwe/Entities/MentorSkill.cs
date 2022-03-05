using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("MentorSkill")]

    public partial class MentorSkill
    {
        public string Id { get; set; }
        public string SkillId { get; set; }
        public string MentorId { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
