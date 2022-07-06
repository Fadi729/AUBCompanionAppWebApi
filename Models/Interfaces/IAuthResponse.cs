namespace CompanionApp.Models.Interfaces;

public interface IAuthResponse
{
    string Token { get; set; }
    bool IsSuccessful { get; set; }
    IEnumerable<string>? ErrorMessages { get; set; }
}