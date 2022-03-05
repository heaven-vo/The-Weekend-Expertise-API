using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MajorId { get; set; }

        public virtual Major Major { get; set; }
    }
}
