namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterNotFoundException : Exception
    {
        public int ErrorCode { get; } = 404;
        
        readonly static string defaultErrorMessage = "Semester Not Found.";

        public SemesterNotFoundException() : base(defaultErrorMessage) { }
        public SemesterNotFoundException(string? message) : base(message) { }
        public SemesterNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
