using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("UserAccount")]
    public partial class User
    {
        public User()
        {
            FcmTokens = new HashSet<FcmToken>();
            Notifications = new HashSet<Notification>();
        }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public Boolean Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<FcmToken> FcmTokens { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
