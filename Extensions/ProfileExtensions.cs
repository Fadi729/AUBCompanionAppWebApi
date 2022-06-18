using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class ProfileExtensions
    {
        
        public static ProfileQuerryDTO ToProfileQuerryDTO(this Profile profile)
        {
            return new ProfileQuerryDTO
            {
                Id        = profile.Id,
                FirstName = profile.FirstName,
                LastName  = profile.LastName,
                Email     = profile.Email,
                Major     = profile.Major,
                Class     = profile.Class
            };
        }
        public static Profile          ToProfile         (this ProfileCommandDTO profile)
        {
            return new Profile
            {
                Id        = Guid.NewGuid(),
                FirstName = profile.FirstName,
                LastName  = profile.LastName,
                Email     = profile.Email,
                Major     = profile.Major,
                Class     = profile.Class,
            };
        }
        public static Profile          ToProfile         (this ProfileCommandDTO profile, Guid id)
        {
            return new Profile
            {
                Id        = id,
                FirstName = profile.FirstName,
                LastName  = profile.LastName,
                Email     = profile.Email,
                Major     = profile.Major,
                Class     = profile.Class,
            };
        }
    }
}
