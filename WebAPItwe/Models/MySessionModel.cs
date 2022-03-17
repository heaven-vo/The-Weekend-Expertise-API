using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class MySessionModel
    {
        public string Id { get; set; }
        public string SubjectImage { get; set; }
        public string SubjectName { get; set; }
        public string Date { get; set; }
        public int Slot { get; set; }
        public string CafeName { get; set; }
        public List<MentorInSessionModel> ListMentor { get; set; }
        public int Status { get; set; }
        public Boolean isLead { get; set; }
    }
}
