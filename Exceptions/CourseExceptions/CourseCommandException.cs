namespace CompanionApp.Exceptions.CourseExceptions
{
    public class CourseCommandException : Exception
    {
        static readonly string _dafualtErrorMessage = "Course Command Failed.";
        public CourseCommandException() : base(_dafualtErrorMessage) { }
        public CourseCommandException(string message) : base(message) { }
        public CourseCommandException(string message, Exception inner) : base(message, inner) { }
    }
}