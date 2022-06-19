namespace CompanionApp.Exceptions.PostExceptions
{
    public class PostNotFoundException : Exception
    {
        readonly static string defaultErrorMessage = "Post Not Found.";

        public PostNotFoundException() : base(defaultErrorMessage) { }

        public PostNotFoundException(string message) : base(message) { }

        public PostNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
