using CompanionApp.Models.Interfaces;

namespace CompanionApp.Models
{
    public partial class Semester : ISemester
    {
        public Semester()
        {
            CourseTakenBy   = new HashSet<CourseTakenBy>();
            Courses         = new HashSet<Course>();
        }

        #region Attributes
        public string Id    { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Year  { get; set; } = null!;
        #endregion

        #region Navigation Properties
        public virtual ICollection<CourseTakenBy> CourseTakenBy { get; set; }
        public virtual ICollection<Course       > Courses       { get; set; }
        #endregion

    }
}