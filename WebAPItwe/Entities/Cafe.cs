using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Cafe")]

    public partial class Cafe
    {
        public Cafe()
        {
            Sessions = new HashSet<Session>();
            Vouchers = new HashSet<Voucher>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string Street { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Distric { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string Price { get; set; }    
        public string Status { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
