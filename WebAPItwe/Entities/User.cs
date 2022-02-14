using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPItwe.Entities
{
    public partial class User
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string Status { get; set; }

        public virtual Role Role { get; set; }
    }
}
