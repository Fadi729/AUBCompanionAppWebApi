﻿namespace CompanionApp.Exceptions.CourseExceptions
{
    public class NoCoursesFoundException : Exception
    {
        public int ErrorCode { get; } = 404;

        readonly static string defaultErrorMessage = "No Courses Found.";

        public NoCoursesFoundException() : base(defaultErrorMessage) { }
        public NoCoursesFoundException(string message) : base(message) { }
        public NoCoursesFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
