namespace CompanionApp.Exceptions.LikesExceptions
{
    public class ProfileAlreadyLikedPostException : Exception
    {
        readonly static string defaultErrorMessage = "Profile Already Liked This Post.";

        public ProfileAlreadyLikedPostException() : base(defaultErrorMessage) { }
        public ProfileAlreadyLikedPostException(string? message) : base(message) { }
        public ProfileAlreadyLikedPostException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
