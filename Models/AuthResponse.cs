using CompanionApp.Models.Interfaces;

namespace CompanionApp.Models
{
    public class AuthResponse : IAuthResponse
    {
        public string Token { get; set; } = null!;
    }
}
