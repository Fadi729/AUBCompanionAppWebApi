namespace CompanionApp.Exceptions.ProfileExceptions
{
    public class ProfileCommandException : Exception
    {
        static readonly string _dafualtErrorMessage = "Profile command failed.";
        public ProfileCommandException() : base(_dafualtErrorMessage) { }
        public ProfileCommandException(string message) : base(message) { }
        public ProfileCommandException(string message, Exception inner) : base(message, inner) { }
    }
}
