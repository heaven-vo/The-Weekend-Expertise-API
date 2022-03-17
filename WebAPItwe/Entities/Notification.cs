using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Entities
{
    [Table("Notification")]
    public partial class Notification
    {
        public string Id { get; set; }
        public string ContentNoti { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
