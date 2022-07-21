namespace CompanionApp.Exceptions;

public class UnauthorizedRequestException : Exception
{
    public int ErrorCode { get; } = (int)System.Net.HttpStatusCode.Unauthorized;
        
    readonly static string _defaultErrorMessage = "Unauthorized Request.";

    public UnauthorizedRequestException() : base(_defaultErrorMessage) { }
    public UnauthorizedRequestException(string? message) : base(message) { }
    public UnauthorizedRequestException(string? message, Exception? innerException)
        : base(message, innerException) { }
}