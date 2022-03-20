using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class FeedbackModel
    {
        public double MentorVoting { get; set; }
        public double CafeVoting { get; set; }
        public string FeedbackOfMentor { get; set; }
        public string FeedbackOfCafe { get; set; }
        public string SessionId { get; set; }
    }
}
