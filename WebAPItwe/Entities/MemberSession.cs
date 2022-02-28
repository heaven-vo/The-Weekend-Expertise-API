using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("MemberSession")]

    public partial class MemberSession
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public double Voting { get; set; }
        public string FeedbackOfMentor { get; set; }
        public string FeedbackOfCafe { get; set; }
        public string DateMentorFeedback { get; set; }
        public string DateCafeFeedback { get; set; }
        public string SessionId { get; set; }
        public string Status { get; set; }

        public virtual Member Member { get; set; }
        public virtual Session Session { get; set; }
    }
}
