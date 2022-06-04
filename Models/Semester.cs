namespace CompanionApp.Models
{
    public partial class Semester
    {
        public Semester()
        {
            CourseTakenBies = new HashSet<CourseTakenBy>();
            Courses         = new HashSet<Course>();
        }

        #region Attributes
        public string Id    { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Year  { get; set; } = null!;
        #endregion

        #region Navigation Properties
        public virtual ICollection<CourseTakenBy> CourseTakenBies { get; set; }
        public virtual ICollection<Course       > Courses         { get; set; }
        #endregion

    }
}
