namespace CompanionApp.Exceptions.CommentExceptions
{
    public class CommentNotFoundException : Exception
    {
        readonly static string defaultErrorMessage = "Comment Not Found.";

        public CommentNotFoundException() : base(defaultErrorMessage) { }
        public CommentNotFoundException(string? message) : base(message) { }
        public CommentNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
