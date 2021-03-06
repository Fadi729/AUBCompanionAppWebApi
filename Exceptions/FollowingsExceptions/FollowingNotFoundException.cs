namespace CompanionApp.Exceptions.FollowingsExceptions
{
    public class FollowingNotFoundException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "Following Not Found.";

        public FollowingNotFoundException() : base(_defaultErrorMessage) { }
        public FollowingNotFoundException(string? message) : base(message) { }
        public FollowingNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
