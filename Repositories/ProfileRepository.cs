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
    public class ProfileRepository : IProfileRepository
    {
        readonly CompanionAppDBContext _context;
        readonly ProfileValidation     validationRules;

        public ProfileRepository(CompanionAppDBContext DBcontext, ProfileValidation validation)
        {
            _context = DBcontext;
            validationRules = validation;
        }
        
        public async Task<ProfileQuerryDTO> GetProfileAsync   (Guid id)
        {
            Profile? profile = await _context.Profiles.FindAsync(id);
            if (profile is null)
            {
                throw new ProfileNotFoundException();
            }

            return profile.ToProfileQuerryDTO();
        }
        public async Task<ProfileQuerryDTO> CreateProfileAsync(ProfileCommandDTO profile)
        {
            #region try block
            try
            {
                await validationRules.ValidateAndThrowAsync(profile);
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
        public async Task                   EditProfileAsync  (Guid id, ProfileCommandDTO profile)
        {
            #region try block
            try
            {
                if (!ProfileExists(id))
                {
                    throw new ProfileNotFoundException();
                }

                await validationRules.ValidateAndThrowAsync(profile);
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
        public async Task                   DeleteProfileAsync(Guid id)
        {
            if (!ProfileExists(id))
            {
                throw new ProfileNotFoundException();
            }

            Profile profile = new() { Id = id };
            _context.Entry(profile).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        bool ProfileExists(Guid id)
        {
            return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        bool ProfileExists(string? email)
        {
            return (_context.Profiles?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}
