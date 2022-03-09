using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class SessionMeetingModel
    {
        public string SessionId { get; set; }
        public int Slot { get; set; }
        public string Date { get; set; }
        public int MaxPerson { get; set; }
        public string MentorName { get; set; }
        public string MentorImage { get; set; }
        public double Price { get; set; }
        public string SubjectName { get; set; }
        public string SubjectImage { get; set; }
        public string CafeName { get; set; }
        public int Status { get; set; }
        public string CafeDistric { get; set; }
        public string CafeStreet { get; set; }
        public List<string> ListMemberImage { get; set; }
    }
}
