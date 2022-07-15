namespace CompanionApp.Exceptions;

public class AuthException : Exception
{
    public int ErrorCode { get; set; } = (int)System.Net.HttpStatusCode.Unauthorized;
    public IEnumerable<string> ErrorMessages { get; set; }

    public AuthException()
    {
    }
    public AuthException(string? message) : base(message)
    {
    }

    public AuthException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
}