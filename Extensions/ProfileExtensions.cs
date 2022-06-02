using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class ProfileExtensions
    {
        /// <summary>
        /// Covert a Profile to ProfileDTO
        /// </summary>
        /// <param name="profile">A Profile instance</param>
        /// <returns></returns>
        public static ProfileDTO ToProfileDTO(this Profile profile)
        {
            return new ProfileDTO
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
        /// Covert a ProfileDTO to Profile
        /// </summary>
        /// <param name="profile">A ProfileDTO instance</param>
        /// <returns></returns>
        public static Profile ToProfile(this ProfileDTO profile)
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
        /// Covert a ProfileDTOPUT to Profile
        /// </summary>
        /// <param name="profile">A ProfileDTOPUT instance</param>
        /// <param name="id">Profile ID</param>
        /// <returns></returns>
        public static Profile ToProfile(this ProfileDTOPUT profile, Guid id)
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
        /// Covert a ProfileDTOPOST to Profile
        /// </summary>
        /// <param name="profile">A ProfileDTOPOST instance</param>
        /// <returns></returns>
        public static Profile ToProfile(this ProfileDTOPOST profile)
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
