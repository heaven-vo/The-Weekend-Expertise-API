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
        public string CafeDistric { get; set; }
        public string CafeStreet { get; set; }
        public string MentorName { get; set; }
        public double Price { get; set; }
        public int MaxPerson { get; set; }
        public int CurrentPerson { get; set; }
        public int isJoin { get; set; }
        public List<string> ListMemberImage { get; set; }
    }
}
