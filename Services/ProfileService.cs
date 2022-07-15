using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Services
{
    public class ProfileService : IProfileService
    {
        readonly CompanionAppDBContext         _context;
        readonly DbSet<Profile>                _dbSet;
        readonly ProfileRegistrationValidation _registrationValidationRules;

        public ProfileService(CompanionAppDBContext DBcontext, ProfileRegistrationValidation registrationValidation)
        {
            _context                     = DBcontext;
            _dbSet                       = DBcontext.Profiles;
            _registrationValidationRules = registrationValidation;
        }

        public async Task<ProfileQueryDTO> GetProfileAsync   (Guid id, CancellationToken cancellationToken)
        {
            Profile? profile = await _dbSet.FindAsync(new object?[] { id }, cancellationToken);
            if (profile is null)
            {
                throw new ProfileNotFoundException();
            }

            return profile.ToProfileQuerryDTO();
        }
        public async Task                  ValidateProfile   (ProfileRegistrationDTO profile,     CancellationToken cancellationToken)
        {
            await _registrationValidationRules.ValidateAndThrowAsync(profile, cancellationToken);
        }
        public async Task<ProfileQueryDTO> CreateProfileAsync(ProfileCommandDTO profile,          CancellationToken cancellationToken)
        {
            #region try block
            try
            {
                Profile newProfile = profile.ToProfile();

                _dbSet.Add(newProfile);
                await _context.SaveChangesAsync(cancellationToken);

                return newProfile.ToProfileQuerryDTO();
            }
            #endregion
            #region catch block
            catch (UniqueConstraintException)
            {
                throw new ProfileAlreadyExistsException("Email Already In Use");
            }
            #endregion
        }
        public async Task                  EditProfileAsync  (Guid id, ProfileCommandDTO profile, CancellationToken cancellationToken)
        {
            if (!await _dbSet.ProfileExists(id, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }

            _dbSet.Attach(profile.ToProfile(id)).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task                  DeleteProfileAsync(Guid id, CancellationToken cancellationToken)
        {
            if (!await _dbSet.ProfileExists(id, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }
            _dbSet.Remove(new Profile() { Id = id });
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}