namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterAlreadyExistsException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Conflict;

        readonly static string _defaultErrorMessage = "Semester Already Exists.";

        public SemesterAlreadyExistsException() : base(_defaultErrorMessage) { }
        public SemesterAlreadyExistsException(string? message) : base(message) { }
        public SemesterAlreadyExistsException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
