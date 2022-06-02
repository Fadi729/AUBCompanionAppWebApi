namespace CompanionApp.Models
{
    public partial class Semester
    {
        public Semester()
        {
            CourseTakenBy = new HashSet<CourseTakenBy>();
            Courses       = new HashSet<Course>();
        }

        public string Id    { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Year  { get; set; } = null!;

        public virtual ICollection<CourseTakenBy> CourseTakenBy { get; set; }
        public virtual ICollection<Course>        Courses       { get; set; }
    }
}
