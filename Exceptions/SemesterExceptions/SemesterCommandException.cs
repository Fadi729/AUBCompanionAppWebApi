namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterCommandException : Exception
    {
        readonly static string defaultErrorMessage = "Semester Command Failed.";

        public SemesterCommandException() : base(defaultErrorMessage) { }
        public SemesterCommandException(string? message) : base(message) { }
        public SemesterCommandException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
