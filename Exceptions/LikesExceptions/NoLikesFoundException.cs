﻿namespace CompanionApp.Exceptions.LikesExceptions
{
    public class NoLikesFoundException : Exception
    {
        readonly static string defaultErrorMessage = "No Likes Found";

        public NoLikesFoundException() : base(defaultErrorMessage) { }
        public NoLikesFoundException(string? message) : base(message) { }
        public NoLikesFoundException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}