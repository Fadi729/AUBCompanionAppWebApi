namespace CompanionApp.Exceptions.ProfileExceptions
{
    [Serializable]
    public class ProfileNotFoundException : Exception
    {
        public int ErrorCode { get; } = 404;
        
        static readonly string __defaultErrorMessage = "Profile Not Found.";

        public ProfileNotFoundException() : base(__defaultErrorMessage) { }
        public ProfileNotFoundException(string message) : base(message) { }
        public ProfileNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
