namespace CompanionApp.Exceptions.FollowingsExceptions
{
    public class NoFollowingsFoundException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "No Followings Found.";

        public NoFollowingsFoundException() : base(_defaultErrorMessage) { }
        public NoFollowingsFoundException(string? message) : base(message) { }
        public NoFollowingsFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
