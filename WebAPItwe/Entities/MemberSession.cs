using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class MemberSession
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string MemberId { get; set; }
        public double Voting { get; set; }
        public string Feedback { get; set; }
        public string SessionId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Session Session { get; set; }
    }
}
