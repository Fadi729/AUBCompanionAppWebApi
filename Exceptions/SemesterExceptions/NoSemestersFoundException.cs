namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class NoSemestersFoundException : Exception
    {
        readonly static string defaultErrorMessage = "No Semesters Found.";

        public NoSemestersFoundException() : base(defaultErrorMessage) { }
        public NoSemestersFoundException(string? message) : base(message) { }
        public NoSemestersFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
