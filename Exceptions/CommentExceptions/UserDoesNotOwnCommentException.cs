namespace CompanionApp.Exceptions.CommentExceptions
{
    public class UserDoesNotOwnCommentException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Unauthorized;

        readonly static string _defaultErrorMessage = "User Does Not Own Comment.";
        public UserDoesNotOwnCommentException() : base(_defaultErrorMessage)
        {
        }

        public UserDoesNotOwnCommentException(string? message) : base(message)
        {
        }

        public UserDoesNotOwnCommentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
