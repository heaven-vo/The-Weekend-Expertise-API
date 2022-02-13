using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class MentorModel
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
        public double Price { get; set; }
        public DateTime Birthday { get; set; }
        public string Status { get; set; }
    }
}
