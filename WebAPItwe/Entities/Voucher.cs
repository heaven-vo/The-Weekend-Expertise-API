using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Voucher")]
    public partial class Voucher
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string DayFilter { get; set; }
        public string HourFilter { get; set; }
        public string DiscountRate { get; set; }
        public string MinPerson { get; set; }
        public string MaxAmount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CafeId { get; set; }
        public Boolean Status { get; set; }

        public virtual Cafe Cafe { get; set; }
    }
}
