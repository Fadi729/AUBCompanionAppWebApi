using CompanionApp.ModelsDTO;

namespace CompanionApp.Repositories.Contracts
{
    public interface IProfileRepository
    {
        Task<ProfileQuerryDTO> GetProfileAsync   (Guid id);
        Task<ProfileQuerryDTO> CreateProfileAsync(ProfileCommandDTO profile);
        Task                   EditProfileAsync  (Guid id, ProfileCommandDTO profile);
        Task                   DeleteProfileAsync(Guid id);
    }
}
