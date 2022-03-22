using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Models
{
    public class NotificationContentModel
    {
        public List<string> listUserId { get; set; }
        public string image { get; set; }
        public string content { get; set; }
    }
}
