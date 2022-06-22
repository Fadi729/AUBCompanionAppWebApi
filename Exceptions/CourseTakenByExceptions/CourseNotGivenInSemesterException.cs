namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseNotGivenInSemesterException : Exception
    {
        public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.NotFound;
        
        readonly static string _defaultErrorMessage = "Course Not Given In Semester.";

        public CourseNotGivenInSemesterException() : base(_defaultErrorMessage) { }
        public CourseNotGivenInSemesterException(string? message) : base(message) { }
        public CourseNotGivenInSemesterException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
