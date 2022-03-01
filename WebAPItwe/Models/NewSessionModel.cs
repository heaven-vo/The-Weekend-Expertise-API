using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class NewSessionModel
    {
        public List<string> ListMentor { get; set; }
        public string CafeId { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string Date { get; set; }
        public int Slot { get; set; }
        public string SubjectId { get; set; }
        public int MaxPerson { get; set; }
        public PaymentModel Payments { get; set; }
    }
}
