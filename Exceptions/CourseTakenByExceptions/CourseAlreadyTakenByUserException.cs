﻿namespace CompanionApp.Exceptions.CourseTakenByExceptions
{
    public class CourseAlreadyTakenByUserException : Exception
    {
        readonly static string defaultErrorMessage = "Course Is Already Taken By User.";

        public CourseAlreadyTakenByUserException() : base(defaultErrorMessage) { }
        public CourseAlreadyTakenByUserException(string? message) : base(message) { }
        public CourseAlreadyTakenByUserException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}