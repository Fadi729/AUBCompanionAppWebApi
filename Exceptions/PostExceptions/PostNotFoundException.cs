namespace CompanionApp.Exceptions.PostExceptions
{
    public class PostNotFoundException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "Post Not Found.";

        public PostNotFoundException() : base(_defaultErrorMessage) { }

        public PostNotFoundException(string message) : base(message) { }

        public PostNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
