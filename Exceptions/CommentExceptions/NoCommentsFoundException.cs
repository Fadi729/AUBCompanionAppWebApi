namespace CompanionApp.Exceptions.CommentExceptions
{
    public class NoCommentsFoundException : Exception
    {
        public int ErrorCode { get; } = 404;

        readonly static string defaultErrorMessage = "No Comments Found.";

        public NoCommentsFoundException() : base(defaultErrorMessage) { }
        public NoCommentsFoundException(string? message) : base(message) { }
        public NoCommentsFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
