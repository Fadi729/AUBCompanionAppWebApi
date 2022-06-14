namespace CompanionApp.Exceptions.ProfileExceptions
{
    public class ProfileCommandException : Exception
    {
        public ProfileCommandException() { }
        public ProfileCommandException(string message) : base(message) { }
        public ProfileCommandException(string message, Exception inner) : base(message, inner) { }
    }
}
