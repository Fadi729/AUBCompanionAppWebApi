using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseTakenBies = new HashSet<CourseTakenBy>();
        }

        public int Crn { get; set; }
        public string? Title { get; set; }
        public string Subject { get; set; } = null!;
        public short Code { get; set; }
        public byte Credits { get; set; }
        public string? Attribute { get; set; }
        public string Days { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Location { get; set; }
        public string? Instructor { get; set; }

        public virtual ICollection<CourseTakenBy> CourseTakenBies { get; set; }
    }
}
