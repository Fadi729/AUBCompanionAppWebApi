using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Exceptions.FollowingsExceptions;

namespace CompanionApp.Services
{
    public class FollowingsService : IFollowingsService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Following>      _dbSetFollowing;
        readonly DbSet<Profile>        _dbSetProfile;
        readonly FollowingsValidation  _followingsValidator;

        public FollowingsService(CompanionAppDBContext context, FollowingsValidation  FollowingsValidator)
        {
            _context             = context;
            _dbSetFollowing      = context.Followings;
            _dbSetProfile        = context.Profiles;
            _followingsValidator = FollowingsValidator;
        }

        public async Task<IEnumerable<IsFollowingDTO>> GetIsFollowing(Guid userID)
        {
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }
            IEnumerable<IsFollowingDTO>? isfollowing = await _dbSetFollowing
                .Include(followings => followings.IsFollowingNavigation)
                .Where  (followings => followings.UserId == userID)
                .Select (followings => followings.ToIsFollowingDTO())
                .ToListAsync();

            if (!isfollowing.Any())
            {
                throw new NoFollowingsFoundException();
            }
            return isfollowing;
        }
        public async Task<IEnumerable<FollowersDTO>>   GetFollowers  (Guid userID)
        {
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }
            IEnumerable<FollowersDTO>? followers = await _dbSetFollowing
                .Include(followings => followings.User)
                .Where  (followings => followings.IsFollowing == userID)
                .Select (followings => followings.ToFollowersDTO())
                .ToListAsync();

            if (!followers.Any())
            {
                throw new NoFollowingsFoundException();
            }
            return followers;
        }
        public async Task                              Follow        (FollowingPOSTDTO following)
        {
            await _followingsValidator.ValidateAndThrowAsync(following);
            if (!await _dbSetProfile.ProfileExists(following.UserId))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(following.IsFollowing))
            {
                throw new ProfileNotFoundException("Profile trying to follow is not found.");
            }

            try
            {
                _dbSetFollowing.Add(following.ToFollowing());
                await _context.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                throw new FollowingAlreadyExistsException();
            }
        }
        public async Task                              Unfollow      (FollowingPOSTDTO following)
        {
            await _followingsValidator.ValidateAndThrowAsync(following);
            if (!await _dbSetProfile.ProfileExists(following.UserId))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(following.IsFollowing))
            {
                throw new ProfileNotFoundException("Profile trying to unfollow is not found.");
            }
            if (!await _dbSetFollowing.FollowingExists(following.UserId, following.IsFollowing))
            {
                throw new FollowingNotFoundException();
            }

            _dbSetFollowing.Remove(following.ToFollowing());
            await _context.SaveChangesAsync();
        }
    }
}
