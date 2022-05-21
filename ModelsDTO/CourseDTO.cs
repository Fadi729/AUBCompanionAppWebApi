namespace CompanionApp.ModelsDTO
{
    public class CourseDTO
    {
        public int Crn { get; set; }
        public string Title { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public short Code { get; set; }
        public byte Credits { get; set; }
        public string? Attribute { get; set; }
        public string? Days { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Location { get; set; }
        public string? Instructor { get; set; }
    }
}
