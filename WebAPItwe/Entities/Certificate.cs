using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Certificate")]
    public partial class Certificate
    {
        public Certificate()
        {
            MentorCertificates = new HashSet<MentorCertificate>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MentorCertificate> MentorCertificates { get; set; }
    }
}
