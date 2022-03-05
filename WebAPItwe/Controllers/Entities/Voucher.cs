using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class Voucher
    {
        public string Id { get; set; }
        public string DayFilter { get; set; }
        public string HourFilter { get; set; }
        public string DiscountRate { get; set; }
        public string MinPerson { get; set; }
        public string MaxAmount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CafeId { get; set; }

        public virtual Cafe Cafe { get; set; }
    }
}
