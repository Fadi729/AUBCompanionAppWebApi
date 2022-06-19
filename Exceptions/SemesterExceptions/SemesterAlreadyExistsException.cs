﻿namespace CompanionApp.Exceptions.SemesterExceptions
{
    public class SemesterAlreadyExistsException : Exception
    {
        readonly static string defaultErrorMessage = "Semester Already Exists.";

        public SemesterAlreadyExistsException() : base(defaultErrorMessage) { }
        public SemesterAlreadyExistsException(string? message) : base(message) { }
        public SemesterAlreadyExistsException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
