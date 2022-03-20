using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;

namespace WebAPItwe.Models
{
    public class SessionDetailModel
    {
        public string SessionId { get; set; }
        public string MajorId { get; set; }
        public string MajorName { get; set; }
        public string SubjectImage { get; set; }
        public string SubjectName { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public int Slot { get; set; }
        public int MaxPerson { get; set; }
        public CafeModel Cafe { get; set; }
        public List<MentorInSessionModel> ListMentor { get; set; }
        public List<MemberInSessionModel> ListMember { get; set; }
        public int isJoin { get; set; }
        public Boolean isLead { get; set; }
        public Boolean isFeed { get; set; }
        public int Status { get; set; }
    }
}
