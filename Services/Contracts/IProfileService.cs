using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IProfileService
    {
        public Task<ProfileQueryDTO> GetProfileAsync   (Guid id);
        public Task                  ValidateProfile   (ProfileCommandDTO profile);
        public Task<ProfileQueryDTO> CreateProfileAsync(ProfileCommandDTO profile);
        public Task                  EditProfileAsync  (Guid id, ProfileCommandDTO profile);
        public Task                  DeleteProfileAsync(Guid id);
    }
}
