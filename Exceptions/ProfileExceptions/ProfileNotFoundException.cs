namespace CompanionApp.Exceptions.ProfileExceptions
{
    [Serializable]
    public class ProfileNotFoundException : Exception
    {
        static readonly string _defaultErrorMessage = "Profile Not Found.";

        public ProfileNotFoundException() : base(_defaultErrorMessage) { }
        public ProfileNotFoundException(string message) : base(message) { }
        public ProfileNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
