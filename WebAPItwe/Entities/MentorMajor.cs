using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("MentorMajor")]
    public partial class MentorMajor
    {
        public string Id { get; set; }
        public string MentorId { get; set; }
        public string MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual Mentor Mentor { get; set; }
    }
}
