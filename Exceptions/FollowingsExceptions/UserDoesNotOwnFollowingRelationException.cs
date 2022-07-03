namespace CompanionApp.Exceptions.FollowingsExceptions
{
    public class UserDoesNotOwnFollowingRelationException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Unauthorized;

        readonly static string _defaultErrorMessage = "User Does Not Own Following Relation .";

        public UserDoesNotOwnFollowingRelationException() : base(_defaultErrorMessage)
        {
        }

        public UserDoesNotOwnFollowingRelationException(string? message) : base(message)
        {
        }

        public UserDoesNotOwnFollowingRelationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
