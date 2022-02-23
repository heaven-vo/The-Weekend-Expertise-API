using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class MemberModel
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string MajorId { get; set; }
        public string Birthday { get; set; }
        public Boolean Status { get; set; }
        public string Grade { get; set; }
    }
}
