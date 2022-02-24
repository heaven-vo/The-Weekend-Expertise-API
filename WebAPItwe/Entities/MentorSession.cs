using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class MentorSession
    {
        public string Id { get; set; }
        public string MentorId { get; set; }
        public string SessionId { get; set; }
        public string RequestDate { get; set; }
        public string AcceptDate { get; set; }
        public string Status { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Session Session { get; set; }
    }
}
