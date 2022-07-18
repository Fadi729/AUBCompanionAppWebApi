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
        readonly IUserService          _userService;

        public FollowingsService(CompanionAppDBContext context, IUserService userService)
        {
            _context        = context;
            _dbSetFollowing = context.Followings;
            _userService    = userService;
        }

        public async Task<IEnumerable<IsFollowingDTO>> GetIsFollowing(Guid userID,                        CancellationToken cancellationToken)
        {
            if (await _userService.GetProfileAsync(userID, cancellationToken) is null)
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
            if (await _userService.GetProfileAsync(userID, cancellationToken) is null)
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
            if (await _userService.GetProfileAsync(userID, cancellationToken) is null)
            {
                throw new ProfileNotFoundException();
            }
            if (await _userService.GetProfileAsync(userToFollowID, cancellationToken) is null)
            {
                throw new ProfileNotFoundException();
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
            if (await _userService.GetProfileAsync(userID, cancellationToken) is null)
            {
                throw new ProfileNotFoundException();
            }
            if (await _userService.GetProfileAsync(userToUnfollowID, cancellationToken) is null)
            {
                throw new ProfileNotFoundException();
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
