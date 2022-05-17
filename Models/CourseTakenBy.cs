using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class CourseTakenBy
    {
        public Guid UserId { get; set; }
        public int CCrn { get; set; }
        public Guid SemesterId { get; set; }
        public string? Grade { get; set; }

        public virtual Course CCrnNavigation { get; set; } = null!;
        public virtual Semester Semester { get; set; } = null!;
        public virtual Profile User { get; set; } = null!;
    }
}
