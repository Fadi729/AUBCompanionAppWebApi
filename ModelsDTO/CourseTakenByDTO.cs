namespace CompanionApp.ModelsDTO
{
    public class CourseTakenByDTO
    {
        public string?             Grade    { get; set; }
        public virtual CourseDTO   Course   { get; set; } = null!;
        public virtual SemesterDTO Semester { get; set; } = null!;
        public virtual ProfileQuerryDTO  User     { get; set; } = null!;
    }
    public class CourseTakenBy_User_DTO
    {
        public string?             Grade    { get; set; }
        public virtual CourseDTO   Course   { get; set; } = null!;
        public virtual SemesterDTO Semester { get; set; } = null!;
    }
    public class CourseTakenBy_Course_DTO
    {
        public string?             Grade    { get; set; }
        public virtual ProfileQuerryDTO  User     { get; set; } = null!;
        public virtual SemesterDTO Semester { get; set; } = null!;
    }
    public class CourseTakenBy_POST_DTO
    {
        public Guid    UserId     { get; set; }
        public int     CCrn       { get; set; }
        public string  SemesterId { get; set; } = null!;
        public string? Grade      { get; set; }
    }
}