using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class CafeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string Street { get; set; }
        public string Distric { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string Price { get; set; }
    }
}
