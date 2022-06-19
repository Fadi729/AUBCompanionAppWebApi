namespace CompanionApp.Exceptions.PostExceptions
{
    public class NoPostsFoundException : Exception
    {
        readonly static string defuautErrorMessage = "No Posts Found.";

        public NoPostsFoundException() : base(defuautErrorMessage) { }
        public NoPostsFoundException(string? message) : base(message) { }
        public NoPostsFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
