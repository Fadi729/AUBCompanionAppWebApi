namespace CompanionApp.Exceptions.CourseExceptions
{
    public class CourseNotFoundException : Exception
    {
        public int ErrorCode { get; } = 404;
        
        readonly static string defaultErrorMessage = "Course Not Found.";
        public CourseNotFoundException() : base(defaultErrorMessage) { }
        public CourseNotFoundException(string message) : base(message) { }
        public CourseNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
