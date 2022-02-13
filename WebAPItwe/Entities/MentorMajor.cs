using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class MentorMajor
    {
        public string Id { get; set; }
        public string MentorId { get; set; }
        public string MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual Mentor Mentor { get; set; }
    }
}
