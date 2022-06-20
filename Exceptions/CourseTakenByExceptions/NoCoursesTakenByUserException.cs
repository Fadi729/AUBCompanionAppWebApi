namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class NoCoursesTakenByUserException : Exception
    {
        public NoCoursesTakenByUserException()
        {
        }

        public NoCoursesTakenByUserException(string? message) : base(message)
        {
        }

        public NoCoursesTakenByUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
