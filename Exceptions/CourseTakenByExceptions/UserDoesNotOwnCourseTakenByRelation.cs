namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class UserDoesNotOwnCourseTakenByRelation : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Unauthorized;

        readonly static string _defaultErrorMessage = "User Does Not Own CourseTakenBy Relation.";
        public UserDoesNotOwnCourseTakenByRelation() : base(_defaultErrorMessage)
        {
        }

        public UserDoesNotOwnCourseTakenByRelation(string? message) : base(message)
        {
        }

        public UserDoesNotOwnCourseTakenByRelation(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
