namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseNotTakenByAnyUserException : Exception
    {
        readonly static string? defaultErrorMessage = "Course Is Not Taken By Any User.";

        public CourseNotTakenByAnyUserException() : base(defaultErrorMessage) { }
        public CourseNotTakenByAnyUserException(string? message) : base(message) { }
        public CourseNotTakenByAnyUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
