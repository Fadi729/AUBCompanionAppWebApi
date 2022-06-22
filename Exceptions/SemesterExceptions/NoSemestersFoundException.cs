namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class NoSemestersFoundException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "No Semesters Found.";

        public NoSemestersFoundException() : base(_defaultErrorMessage) { }
        public NoSemestersFoundException(string? message) : base(message) { }
        public NoSemestersFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
