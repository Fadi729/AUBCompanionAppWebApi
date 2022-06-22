namespace CompanionApp.Exceptions.ProfileExceptions
{
    public class ProfileAlreadyExistsException : Exception
    {
        public static int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Conflict;
        
        readonly static string _defaultErrorMessage = "Profile Already Exists.";
        public ProfileAlreadyExistsException() : base(_defaultErrorMessage) { }
        public ProfileAlreadyExistsException(string message) : base(message) { }
        public ProfileAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
