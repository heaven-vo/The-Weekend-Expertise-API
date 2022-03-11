using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class SessionCafeModel
    {
        public string SessionId { get; set; }
        public int Slot { get; set; }
        public string Date { get; set; }
        public int MaxPerson { get; set; }
        public string MentorName { get; set; }
        public string MentorImage { get; set; }
        public string Price { get; set; }
        public string SubjectName { get; set; }
        public string SubjectImage { get; set; }
        public string CafeName { get; set; }
        public int Status { get; set; }
        public string CafeDistric { get; set; }
        public string CafeStreet { get; set; }
        public Boolean CafeActive { get; set; }
        public List<string> ListMemberImage { get; set; }
    }
}
