﻿namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseNotTakenByUserException : Exception
    {
        readonly static string defaultErrorMessage = "Course Is Not Taken By User.";

        public CourseNotTakenByUserException() : base(defaultErrorMessage) { }
        public CourseNotTakenByUserException(string? message) : base(message) { }
        public CourseNotTakenByUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}