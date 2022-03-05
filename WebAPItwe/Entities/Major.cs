using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebAPItwe.Entities
{
    [Table("Major")]
    public partial class Major
    {
        public Major()
        {
            Members = new HashSet<Member>();
            MentorMajors = new HashSet<MentorMajor>();
            Subjects = new HashSet<Subject>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<MentorMajor> MentorMajors { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
