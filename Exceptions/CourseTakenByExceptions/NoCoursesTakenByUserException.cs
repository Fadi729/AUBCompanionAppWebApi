namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class NoCoursesTakenByUserException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;

        readonly static string __defaultErrorMessage = "No Courses Taken By User";
        public NoCoursesTakenByUserException() : base(__defaultErrorMessage) { }
        public NoCoursesTakenByUserException(string? message) : base(message) { }
        public NoCoursesTakenByUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
