using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Entities
{
    [Table("MajorSkill")]
    public partial class MajorSkill
    {
        public string Id { get; set; }
        public string SkillId { get; set; }
        public string MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
