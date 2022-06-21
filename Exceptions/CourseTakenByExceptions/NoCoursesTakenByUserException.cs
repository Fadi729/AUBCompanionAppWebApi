namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class NoCoursesTakenByUserException : Exception
    {
        public int ErrorCode { get; } = 404;

        readonly static string _defaultErrorMessage = "No Courses Taken By User";
        public NoCoursesTakenByUserException() : base(_defaultErrorMessage) { }
        public NoCoursesTakenByUserException(string? message) : base(message) { }
        public NoCoursesTakenByUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
