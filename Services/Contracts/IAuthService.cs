using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(ProfileRegistrationDTO user);
        Task<AuthResponse> LoginAsync   (ProfileLoginDTO user);
    }
}
