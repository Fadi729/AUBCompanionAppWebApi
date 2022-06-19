using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IProfileService
    {
        Task<ProfileQueryDTO> GetProfileAsync   (Guid id);
        Task<ProfileQueryDTO> CreateProfileAsync(ProfileCommandDTO profile);
        Task                  EditProfileAsync  (Guid id, ProfileCommandDTO profile);
        Task                  DeleteProfileAsync(Guid id);
    }
}
