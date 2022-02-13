using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class Payment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string SessionId { get; set; }

        public virtual Session Session { get; set; }
    }
}
