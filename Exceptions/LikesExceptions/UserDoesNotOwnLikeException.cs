namespace CompanionApp.Exceptions.LikesExceptions
{
    public class UserDoesNotOwnLikeException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Unauthorized;

        readonly static string _defaultErrorMessage = "User Does Not Own Like.";

        public UserDoesNotOwnLikeException() : base(_defaultErrorMessage)
        {
        }
        public UserDoesNotOwnLikeException(string? message) : base(message)
        {
        }
        public UserDoesNotOwnLikeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
