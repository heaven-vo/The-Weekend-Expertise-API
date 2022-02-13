using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class Member
    {
        public Member()
        {
            MemberSessions = new HashSet<MemberSession>();
            Sessions = new HashSet<Session>();
        }

        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string MajorId { get; set; }
        public DateTime Birthday { get; set; }
        public string Status { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<MemberSession> MemberSessions { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
