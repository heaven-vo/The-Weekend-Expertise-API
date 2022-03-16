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
            SkillSubjects = new HashSet<SkillSubject>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Boolean Status { get; set; }

        public virtual ICollection<MentorSkill> MentorSkills { get; set; }
        public virtual ICollection<SkillSubject> SkillSubjects { get; set; }
    }
}
