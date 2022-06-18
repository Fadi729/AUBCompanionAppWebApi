using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Services
{
    public class ProfileService : IProfileService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Profile>        _dbSet;
        readonly ProfileValidation     _validationRules;

        public ProfileService(CompanionAppDBContext DBcontext, ProfileValidation validation)
        {
            _context         = DBcontext;
            _dbSet           = DBcontext.Profiles;
            _validationRules = validation;
        }
        
        public async Task<ProfileQueryDTO> GetProfileAsync   (Guid id)
        {
            Profile? profile = await _dbSet.FindAsync(id);
            if (profile is null)
            {   
                throw new ProfileNotFoundException();
            }

            return profile.ToProfileQuerryDTO();
        }
        public async Task<ProfileQueryDTO> CreateProfileAsync(ProfileCommandDTO profile)
        {
            #region try block
            try
            {
                await _validationRules.ValidateAndThrowAsync(profile);
                if (_dbSet.ProfileExists(profile.Email))
                {
                    throw new ProfileAlreadyExistsException("Email Already In Use");
                }
                Profile newProfile = profile.ToProfile();

                _dbSet.Add(newProfile);
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
        public async Task                  EditProfileAsync  (Guid id, ProfileCommandDTO profile)
        {
            #region try block
            try
            {
                if (!_dbSet.ProfileExists(id))
                {
                    throw new ProfileNotFoundException();
                }

                await _validationRules.ValidateAndThrowAsync(profile);
                _dbSet.Attach(profile.ToProfile(id)).State = EntityState.Modified;
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
        public async Task                  DeleteProfileAsync(Guid id)
        {
            if (!_dbSet.ProfileExists(id))
            {
                throw new ProfileNotFoundException();
            }
            _dbSet.Remove(new Profile() { Id = id });
            await _context.SaveChangesAsync();
        }
    }
}
