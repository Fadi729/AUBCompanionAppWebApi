namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterNotFoundException : Exception
    {
        public int ErrorCode { get; } = 404;
        
        readonly static string _defaultErrorMessage = "Semester Not Found.";

        public SemesterNotFoundException() : base(_defaultErrorMessage) { }
        public SemesterNotFoundException(string? message) : base(message) { }
        public SemesterNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
