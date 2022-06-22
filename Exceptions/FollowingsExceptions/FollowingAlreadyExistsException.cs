namespace CompanionApp.Exceptions.FollowingsExceptions
{
    public class FollowingAlreadyExistsException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Conflict;
        
        readonly static string __defaultErrorMessage = "Following Already Exists";

        public FollowingAlreadyExistsException() : base(__defaultErrorMessage) { }
        public FollowingAlreadyExistsException(string? message) : base(message) { }
        public FollowingAlreadyExistsException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
