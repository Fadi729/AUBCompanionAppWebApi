﻿namespace CompanionApp.Exceptions.FollowingsExceptions
{
    public class FollowingAlreadyExistsException : Exception
    {
        readonly static string _defaultErrorMessage = "Following Already Exists";

        public FollowingAlreadyExistsException() : base(_defaultErrorMessage) { }
        public FollowingAlreadyExistsException(string? message) : base(message) { }
        public FollowingAlreadyExistsException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}