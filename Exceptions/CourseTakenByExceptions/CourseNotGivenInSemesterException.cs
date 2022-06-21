namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseNotGivenInSemesterException : Exception
    {
        public int ErrorCode { get; } = 404;
        
        readonly static string _defaultErrorMessage = "Course Not Given In Semester.";

        public CourseNotGivenInSemesterException() : base(_defaultErrorMessage) { }
        public CourseNotGivenInSemesterException(string? message) : base(message) { }
        public CourseNotGivenInSemesterException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
