using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPItwe.Entities
{
    [Table("MentorCertificate")]
    public class MentorCertificate
    {
        public string Id { get; set; }
        public string CertificateId { get; set; }
        public string MentorId { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Certificate Certificate { get; set; }

    }
}
