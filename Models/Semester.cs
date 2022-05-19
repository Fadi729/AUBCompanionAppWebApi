using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Semester
    {
        public Semester()
        {
            CourseTakenBies = new HashSet<CourseTakenBy>();
        }

        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Year { get; set; }

        public virtual ICollection<CourseTakenBy> CourseTakenBies { get; set; }
    }
}
