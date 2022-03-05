using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Session")]

    public partial class Session
    {
        public Session()
        {
            MemberSessions = new HashSet<MemberSession>();
            MentorSessions = new HashSet<MentorSession>();
            Payments = new HashSet<Payment>();
        }

        public string Id { get; set; }
        public int Slot { get; set; }
        public string Date { get; set; }
        public string DateCreated { get; set; }
        public int MaxPerson { get; set; }
        public string MentorId { get; set; }
        public string MentorName { get; set; }
        public string MemberId { get; set; }
        public string MajorId { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string CafeId { get; set; }
        public string Status { get; set; }

        public virtual Cafe Cafe { get; set; }
        public virtual Member Member { get; set; }
        public virtual Mentor Mentor { get; set; }
        public virtual ICollection<MemberSession> MemberSessions { get; set; }
        public virtual ICollection<MentorSession> MentorSessions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
