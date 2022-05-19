using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class ProfileExtensions
    {
        //covert a Profile to ProfileDTO
        public static ProfileDTO ToProfileDTO(this Profile profile)
        {
            return new ProfileDTO
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Major = profile.Major,
                Class = profile.Class,
            };
        }

        public static Profile ToProfile(this ProfileDTO profile)
        {
            return new Profile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Major = profile.Major,
                Class = profile.Class,
            };
        }
    }
}
