namespace CompanionApp.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseTakenBies = new HashSet<CourseTakenBy>();
        }

        #region Attributes
        public int       Crn           { get; set; }
        public string    Title         { get; set; } = null!;
        public string    Subject       { get; set; } = null!;
        public string    Code          { get; set; } = null!;
        public byte      Credits       { get; set; }
        public string?   Section       { get; set; }
        public string?   Attribute     { get; set; }
        public string?   Days1         { get; set; }
        public TimeSpan? StartTime1    { get; set; }
        public TimeSpan? EndTime1      { get; set; }
        public string?   Location1     { get; set; }
        public string?   Type1         { get; set; }
        public string?   Instructor1   { get; set; }
        public string?   Days2         { get; set; }
        public TimeSpan? StartTime2    { get; set; }
        public TimeSpan? EndTime2      { get; set; }
        public string?   Location2     { get; set; }
        public string?   Type2         { get; set; }
        public string?   Instructor2   { get; set; }
        public string    SemesterId    { get; set; } = null!;
        public string?   Prerequisites { get; set; }
        public string?   Restrictions  { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Semester                   Semester        { get; set; } = null!;
        public virtual ICollection<CourseTakenBy> CourseTakenBies { get; set; }
        #endregion
    }
}
