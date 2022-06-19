namespace CompanionApp.Exceptions.FollowingsExceptions
{
    public class NoFollowingsFoundException : Exception
    {
        readonly static string defaultErrorMessage = "No Followings Found.";

        public NoFollowingsFoundException() : base(defaultErrorMessage) { }
        public NoFollowingsFoundException(string? message) : base(message) { }
        public NoFollowingsFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
