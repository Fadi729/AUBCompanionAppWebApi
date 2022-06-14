using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class ProfileExtensions
    {
        /// <summary>
        /// Covert a Profile to ProfileQuerryDTO
        /// </summary>
        /// <param name="profile">A Profile instance</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Covert a ProfileQuerryDTO to Profile
        /// </summary>
        /// <param name="profile">A ProfileQuerryDTO instance</param>
        /// <returns></returns>
        public static Profile ToProfile(this ProfileQuerryDTO profile)
        {
            return new Profile
            {
                Id        = profile.Id,
                FirstName = profile.FirstName,
                LastName  = profile.LastName,
                Email     = profile.Email,
                Major     = profile.Major,
                Class     = profile.Class,
            };
        }

        /// <summary>
        /// Covert a ProfileQuerryDTOPUT to Profile
        /// </summary>
        /// <param name="profile">A ProfileQuerryDTOPUT instance</param>
        /// <param name="id">Profile ID</param>
        /// <returns></returns>
        public static Profile ToProfile(this ProfileCommandDTO profile, Guid id)
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

        /// <summary>
        /// Covert a ProfileQuerryDTOPOST to Profile
        /// </summary>
        /// <param name="profile">A ProfileQuerryDTOPOST instance</param>
        /// <returns></returns>
        public static Profile ToProfile(this ProfileCommandDTO profile)
        {
            return new Profile
            {
                FirstName = profile.FirstName,
                LastName  = profile.LastName,
                Email     = profile.Email,
                Major     = profile.Major,
                Class     = profile.Class,
            };
        }
    }
}
