namespace CompanionApp.ModelsDTO
{
    public class CourseDTO
    {
        public string  Crn             { get; set; } = null!;
        public string  Title           { get; set; } = null!;
        public string  Subject         { get; set; } = null!;
        public string  Code            { get; set; } = null!;
        public string? Credits         { get; set; }
        public string? Section         { get; set; }
        public string? Attribute       { get; set; }
        public string? Levels          { get; set; }
        public string? Days1           { get; set; }
        public string? StartTime1      { get; set; }
        public string? EndTime1        { get; set; }
        public string? Location1       { get; set; }
        public string? Instructor1     { get; set; }
        public string? Type1           { get; set; }
        public string? Days2           { get; set; }
        public string? StartTime2      { get; set; }
        public string? EndTime2        { get; set; }
        public string? Location2       { get; set; }
        public string? Instructor2     { get; set; }
        public string? Type2           { get; set; }
        public string  SemesterId      { get; set; } = null!;
        public string? Prerequisites   { get; set; }
        public string? Corequisites    { get; set; }
        public string? MutualExclusion { get; set; }
        public string? Restrictions    { get; set; }
    }
}
