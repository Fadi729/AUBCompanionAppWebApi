using CompanionApp.Extensions;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Repositories.Contracts;
using CompanionApp.Exceptions.ProfileExceptions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Validation;
using FluentValidation;

namespace CompanionApp.Repositories
{
    public class ProfileRepository : IProfileRespository
    {
        private readonly CompanionAppDBContext _context;
        private readonly ProfileValidation     validationRules = new();

        public ProfileRepository(CompanionAppDBContext DBcontext)
        {
            _context = DBcontext;
        }
        public async Task<ProfileQuerryDTO> GetProfile   (Guid id)
        {
            Profile? profile = await _context.Profiles.FindAsync(id);
            if (profile is null)
            {
                throw new ProfileNotFoundException("Profile Does Not Exist");
            }

            return profile.ToProfileQuerryDTO();
        }
        public async Task<ProfileQuerryDTO> CreateProfile(ProfileCommandDTO profile)
        {
            #region try block
            try
            {
                validationRules.ValidateAndThrow(profile);
                if (ProfileExists(profile.Email))
                {
                    throw new ProfileAlreadyExistsException("Email Already In Use");
                }
                Profile newProfile = profile.ToProfile();
                newProfile.Id = Guid.NewGuid();

                _context.Entry(profile.ToProfile(newProfile.Id)).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return newProfile.ToProfileQuerryDTO();
            }
            #endregion
            #region catch block
            catch (ValidationException ex)
            {
                throw new ProfileCommandException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
            #endregion
        }
        public async Task                   EditProfile  (Guid id, ProfileCommandDTO profile)
        {
            #region try block
            try
            {
                if (!ProfileExists(id))
                {
                    throw new ProfileNotFoundException("Profile Does Not Exist");
                }

                validationRules.ValidateAndThrow(profile);
                _context.Entry(profile.ToProfile(id)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            #endregion
            #region catch block
            catch (ValidationException ex)
            {
                throw new ProfileCommandException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
            #endregion
        }
        public async Task                   DeleteProfile(Guid id)
        {
            if (!ProfileExists(id))
            {
                throw new ProfileNotFoundException("Profile Does Not Exist");
            }

            Profile profile = new() { Id = id };
            _context.Entry(profile).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        private bool ProfileExists(Guid id)
        {
            return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool ProfileExists(string? email)
        {
            return (_context.Profiles?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}
