namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseNotTakenByUserException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "Course Is Not Taken By User.";

        public CourseNotTakenByUserException() : base(_defaultErrorMessage) { }
        public CourseNotTakenByUserException(string? message) : base(message) { }
        public CourseNotTakenByUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
