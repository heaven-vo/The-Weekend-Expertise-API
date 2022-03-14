using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Entities
{
    [Table("SkillSubject")]

    public partial class SkillSubject
    {
        public string Id { get; set; }
        public string SkillId { get; set; }
        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
