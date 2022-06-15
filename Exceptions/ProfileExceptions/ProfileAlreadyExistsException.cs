namespace CompanionApp.Exceptions.ProfileExceptions
{
    public class ProfileAlreadyExistsException : Exception
    {
        readonly static string defaultErrorMessage = "Profile Already Exists.";
        public ProfileAlreadyExistsException() : base(defaultErrorMessage) { }
        public ProfileAlreadyExistsException(string message) : base(message) { }
        public ProfileAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
