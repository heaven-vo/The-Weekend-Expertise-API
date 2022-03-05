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
        public string Slogan { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
        public double Price { get; set; }
        public string Birthday { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
        public List<string> ListMajor { get; set; }
    }
}
