namespace CompanionApp.Exceptions.LikesExceptions
{
    public class LikeNotFoundException : Exception
    {
        readonly static string defaultErrorMessage = "Like Not Found.";

        public LikeNotFoundException() : base(defaultErrorMessage) { }
        public LikeNotFoundException(string? message) : base(message) { }
        public LikeNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
