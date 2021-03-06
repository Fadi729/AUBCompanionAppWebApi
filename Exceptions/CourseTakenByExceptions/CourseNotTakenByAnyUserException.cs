namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseNotTakenByAnyUserException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "Course Is Not Taken By Any User.";

        public CourseNotTakenByAnyUserException() : base(_defaultErrorMessage) { }
        public CourseNotTakenByAnyUserException(string? message) : base(message) { }
        public CourseNotTakenByAnyUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
