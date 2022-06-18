using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IProfileService
    {
        Task<ProfileQuerryDTO> GetProfileAsync   (Guid id);
        Task<ProfileQuerryDTO> CreateProfileAsync(ProfileCommandDTO profile);
        Task                   EditProfileAsync  (Guid id, ProfileCommandDTO profile);
        Task                   DeleteProfileAsync(Guid id);
    }
}
