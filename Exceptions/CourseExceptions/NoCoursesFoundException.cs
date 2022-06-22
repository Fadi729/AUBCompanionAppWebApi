namespace CompanionApp.Exceptions.CourseExceptions
{
    public class NoCoursesFoundException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;

        readonly static string _defaultErrorMessage = "No Courses Found.";

        public NoCoursesFoundException() : base(_defaultErrorMessage) { }
        public NoCoursesFoundException(string message) : base(message) { }
        public NoCoursesFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
