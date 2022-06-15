namespace CompanionApp.Exceptions.CourseExceptions
{
    public class CourseNotFoundException : Exception
    {
        readonly static string defaultErrorMessage = "Course Not Found.";
        public CourseNotFoundException() : base(defaultErrorMessage) { }
        public CourseNotFoundException(string message) : base(message) { }
        public CourseNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
