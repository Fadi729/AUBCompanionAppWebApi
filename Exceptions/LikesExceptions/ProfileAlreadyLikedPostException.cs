namespace CompanionApp.Exceptions.LikesExceptions
{
    public class ProfileAlreadyLikedPostException : Exception
    {
        public int ErrorCode { get; } = 409;
        
        readonly static string _defaultErrorMessage = "Profile Already Liked This Post.";

        public ProfileAlreadyLikedPostException() : base(_defaultErrorMessage) { }
        public ProfileAlreadyLikedPostException(string? message) : base(message) { }
        public ProfileAlreadyLikedPostException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
