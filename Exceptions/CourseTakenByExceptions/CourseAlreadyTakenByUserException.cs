namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseAlreadyTakenByUserException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Conflict;
        
        readonly static string _defaultErrorMessage = "Course Is Already Taken By User.";

        public CourseAlreadyTakenByUserException() : base(_defaultErrorMessage) { }
        public CourseAlreadyTakenByUserException(string? message) : base(message) { }
        public CourseAlreadyTakenByUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
