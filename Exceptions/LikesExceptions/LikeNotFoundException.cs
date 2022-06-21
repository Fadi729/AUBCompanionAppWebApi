namespace CompanionApp.Exceptions.LikesExceptions
{
    public class LikeNotFoundException : Exception
    {
        public int ErrorCode { get; } = 404;        
        
        readonly static string _defaultErrorMessage = "Like Not Found.";

        public LikeNotFoundException() : base(_defaultErrorMessage) { }
        public LikeNotFoundException(string? message) : base(message) { }
        public LikeNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
