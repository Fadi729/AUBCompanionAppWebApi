namespace CompanionApp.Exceptions.PostExceptions
{
    public class UserDoesNotOwnPostException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Unauthorized;

        readonly static string _defaultErrorMessage = "User Does Not Own The Post.";
        public UserDoesNotOwnPostException() : base(_defaultErrorMessage)
        {
        }

        public UserDoesNotOwnPostException(string? message) : base(message)
        {
        }

        public UserDoesNotOwnPostException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
