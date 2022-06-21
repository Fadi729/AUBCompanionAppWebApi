namespace CompanionApp.Exceptions.CommentExceptions
{
    public class NoCommentsFoundException : Exception
    {
        public int ErrorCode { get; } = 404;

        readonly static string _defaultErrorMessage = "No Comments Found.";

        public NoCommentsFoundException() : base(_defaultErrorMessage) { }
        public NoCommentsFoundException(string? message) : base(message) { }
        public NoCommentsFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
