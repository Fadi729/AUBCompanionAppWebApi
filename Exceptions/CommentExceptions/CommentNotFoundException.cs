namespace CompanionApp.Exceptions.CommentExceptions
{
    public class CommentNotFoundException : Exception
    {
        public int ErrorCode { get; } = 404;

        readonly static string defaultErrorMessage = "Comment Not Found.";

        public CommentNotFoundException() : base(defaultErrorMessage) { }
        public CommentNotFoundException(string? message) : base(message) { }
        public CommentNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
