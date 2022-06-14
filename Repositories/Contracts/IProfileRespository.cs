using CompanionApp.ModelsDTO;

namespace CompanionApp.Repositories.Contracts
{
    public interface IProfileRespository
    {
        Task<ProfileQuerryDTO>  GetProfile   (Guid id);
        Task<ProfileQuerryDTO> CreateProfile(ProfileCommandDTO profile);
        Task                    EditProfile  (Guid id, ProfileCommandDTO profile);
        Task                    DeleteProfile(Guid id);
    }
}
