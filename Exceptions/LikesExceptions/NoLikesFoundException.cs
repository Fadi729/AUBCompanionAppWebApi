namespace CompanionApp.Exceptions.LikesExceptions
{
    public class NoLikesFoundException : Exception
    {
        public int ErrorCode { get; } = 404;
        
        readonly static string _defaultErrorMessage = "No Likes Found";

        public NoLikesFoundException() : base(_defaultErrorMessage) { }
        public NoLikesFoundException(string? message) : base(message) { }
        public NoLikesFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
