namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterNotFoundException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "Semester Not Found.";

        public SemesterNotFoundException() : base(_defaultErrorMessage) { }
        public SemesterNotFoundException(string? message) : base(message) { }
        public SemesterNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
