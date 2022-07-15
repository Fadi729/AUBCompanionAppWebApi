using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
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

        public FollowingsService(CompanionAppDBContext context)
        {
            _context        = context;
            _dbSetFollowing = context.Followings;
            _dbSetProfile   = context.Profiles;
        }

        public async Task<IEnumerable<IsFollowingDTO>> GetIsFollowing(Guid userID,                        CancellationToken cancellationToken)
        {
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }
            IEnumerable<IsFollowingDTO>? isfollowing = await _dbSetFollowing
                .Include(followings => followings.IsFollowingNavigation)
                .Where  (followings => followings.UserId == userID)
                .Select (followings => followings.ToIsFollowingDTO())
                .ToListAsync(cancellationToken);

            if (!isfollowing.Any())
            {
                throw new NoFollowingsFoundException();
            }
            return isfollowing;
        }
        public async Task<IEnumerable<FollowersDTO>>   GetFollowers  (Guid userID,                        CancellationToken cancellationToken)
        {
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }
            IEnumerable<FollowersDTO>? followers = await _dbSetFollowing
                .Include(followings => followings.User)
                .Where  (followings => followings.IsFollowing == userID)
                .Select (followings => followings.ToFollowersDTO())
                .ToListAsync(cancellationToken);

            if (!followers.Any())
            {
                throw new NoFollowingsFoundException();
            }
            return followers;
        }
        public async Task<FollowingPOSTDTO>            Follow        (Guid userID, Guid userToFollowID,   CancellationToken cancellationToken)
        {
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(userToFollowID, cancellationToken))
            {
                throw new ProfileNotFoundException("Profile trying to follow is not found.");
            }

            try
            {
                Following follow = new()
                {
                    UserId      = userID,
                    IsFollowing = userToFollowID
                };
                _dbSetFollowing.Add(follow);
                await _context.SaveChangesAsync(cancellationToken);
                return follow.ToFollowingPOSTDTO();
            }
            catch (UniqueConstraintException)
            {
                throw new FollowingAlreadyExistsException();
            }
        }
        public async Task                              Unfollow      (Guid userID, Guid userToUnfollowID, CancellationToken cancellationToken)
        {
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(userToUnfollowID, cancellationToken))
            {
                throw new ProfileNotFoundException("Profile trying to unfollow is not found.");
            }

            Following? following = await _dbSetFollowing.GetFollowingAsync(userID, userToUnfollowID, cancellationToken);
            if (following is null)
            {
                throw new FollowingNotFoundException();
            }
            if (following.UserId != userID)
            {
                throw new UserDoesNotOwnFollowingRelationException();
            }

            _dbSetFollowing.Remove(following);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
