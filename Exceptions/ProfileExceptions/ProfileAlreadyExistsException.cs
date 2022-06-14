namespace CompanionApp.Exceptions.ProfileExceptions
{
    public class ProfileAlreadyExistsException : Exception
    {
        public ProfileAlreadyExistsException() { }
        public ProfileAlreadyExistsException(string message) : base(message) { }
        public ProfileAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
