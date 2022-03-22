using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class SessionTodayModel
    {
        public string Id { get; set; }
        public string SessionImage { get; set; }
        public string SessionName { get; set; }
        public string Date { get; set; }
        public int Slot { get; set; }
        public MentorInSessionModel Mentor { get; set; }
        public string CafeName { get; set; }

    }
}
