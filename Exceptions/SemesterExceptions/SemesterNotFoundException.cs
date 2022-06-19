using System.Runtime.Serialization;

namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterNotFoundException : Exception
    {
        readonly static string defaultErrorMessage = "Semester Not Found";

        public SemesterNotFoundException() : base(defaultErrorMessage) { }
        public SemesterNotFoundException(string? message) : base(message) { }
        public SemesterNotFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
