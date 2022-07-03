namespace CompanionApp.Exceptions.CommentExceptions
{
    public class CommentDoesNotBelongToPostException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.BadRequest;

        readonly static string _defaultErrorMessage = "Comment Does Not Belong To Post.";
        public CommentDoesNotBelongToPostException() : base(_defaultErrorMessage)
        {
        }
        public CommentDoesNotBelongToPostException(string? message) : base(message)
        {
        }

        public CommentDoesNotBelongToPostException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
