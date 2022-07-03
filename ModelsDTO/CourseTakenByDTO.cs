namespace CompanionApp.ModelsDTO
{
    public class CourseTakenBy_User_DTO
    {
        public string?             Grade    { get; set; }
        public virtual CourseDTO   Course   { get; set; } = null!;
        public virtual SemesterDTO Semester { get; set; } = null!;
    }
    public class CourseTakenBy_Course_DTO
    {
        public string?                  Grade    { get; set; }
        public virtual ProfileQueryDTO  User     { get; set; } = null!;
        public virtual SemesterDTO      Semester { get; set; } = null!;
    }
    public class CourseTakenBy_POST_DTO
    {
        public string  UserId     { get; set; } = null!;
        public string  CCrn       { get; set; } = null!;
        public string  SemesterId { get; set; } = null!;
        public string? Grade      { get; set; }
    }
}