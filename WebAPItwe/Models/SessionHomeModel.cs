using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class SessionHomeModel
    {
        public string SessionId { get; set; }
        public string SubjectImage { get; set; }
        public string SubjectName { get; set; }
        public string Date { get; set; }
        public int Slot { get; set; }
        public string CafeName { get; set; }
        public string MentorName { get; set; }
        public double Price { get; set; }
        public Boolean isJoin { get; set; }
        public List<string> ListMemberImage { get; set; }
    }
}
