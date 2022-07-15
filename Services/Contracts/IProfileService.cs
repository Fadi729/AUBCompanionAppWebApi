using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IProfileService
    {
        public Task<ProfileQueryDTO> GetProfileAsync   (Guid id, CancellationToken cancellationToken);
        public Task                  ValidateProfile   (ProfileRegistrationDTO profile,     CancellationToken cancellationToken);
        public Task<ProfileQueryDTO> CreateProfileAsync(ProfileCommandDTO profile,          CancellationToken cancellationToken);
        public Task                  EditProfileAsync  (Guid id, ProfileCommandDTO profile, CancellationToken cancellationToken);
        public Task                  DeleteProfileAsync(Guid id, CancellationToken cancellationToken);
    }
}
