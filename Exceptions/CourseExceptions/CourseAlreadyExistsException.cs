namespace CompanionApp.Exceptions.CourseExceptions
{
    public class CourseAlreadyExistsException : Exception
    {
        public int ErrorCode { get; } = 409;

        readonly static string defaultErrorMessage = "Course Already Exists.";

        public CourseAlreadyExistsException() : base(defaultErrorMessage) { }
        public CourseAlreadyExistsException(string message) : base(message) { }
        public CourseAlreadyExistsException(string message, Exception inner) : base(message, inner)
        { }
    }
}
