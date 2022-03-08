using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class HistoryModel
    {
        public string SessionId { get; set; }
        public string SubjectName { get; set; }
        public int Slot { get; set; }
        public string Date { get; set; }
        public string MentorName { get; set; }
        public int Status { get; set; }
    }
}
