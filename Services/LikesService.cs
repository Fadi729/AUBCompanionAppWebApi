using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
using CompanionApp.Exceptions.LikesExceptions;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Services
{
    public class LikesService : ILikesService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Like>           _dbSetLikes;
        readonly DbSet<Profile>        _dbSetProfile;
        readonly DbSet<Post>           _dbSetPost;

        public LikesService(CompanionAppDBContext context)
        {
            _context      = context;
            _dbSetLikes   = _context.Likes;
            _dbSetProfile = _context.Profiles;
            _dbSetPost    = _context.Posts;
        }

        public async Task<IEnumerable<LikeDTOUsers>> GetPostLikes     (Guid postID,              CancellationToken cancellationToken)
        {
            if (!await _dbSetPost.PostExists(postID, cancellationToken))
            {
                throw new PostNotFoundException();
            }

            IEnumerable<LikeDTOUsers> likes = await _dbSetLikes
                .Include(like => like.User)
                .Where  (like => like.PostId == postID)
                .Select (like => like.ToLikeDTOUsers())
                .ToListAsync(cancellationToken);

            if (!likes.Any())
            {
                throw new NoLikesFoundException();
            }
            return likes;
        }
        public async Task<int>                       GetPostLikesCount(Guid postID,              CancellationToken cancellationToken)
        {
            if (!await _dbSetPost.PostExists(postID, cancellationToken))
            {
                throw new PostNotFoundException();
            }

            int likes = await _dbSetLikes
                .Where(like => like.PostId == postID)
                .CountAsync(cancellationToken);

            if (likes == 0)
            {
                throw new NoLikesFoundException();
            }
            return likes;
        }
        public async Task<LikeQueryDTO>              LikePost         (Guid postID, Guid userID, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _dbSetPost.PostExists(postID, cancellationToken))
                {
                    throw new PostNotFoundException();
                }
                if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
                {
                    throw new ProfileNotFoundException();
                }

                Like newlike = new()
                {
                    PostId    = postID,
                    UserId    = userID,
                    DateLiked = DateTime.Now

                };
                _dbSetLikes.Add(newlike);
                await _context.SaveChangesAsync(cancellationToken);
                return newlike.ToLikeDTO();
            }
            catch (UniqueConstraintException)
            {
                throw new ProfileAlreadyLikedPostException();
            }
        }
        public async Task                            UnlikePost       (Guid postID, Guid userID, CancellationToken cancellationToken)
        {
            if (!await _dbSetPost.PostExists(postID, cancellationToken))
            {
                throw new PostNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileAlreadyExistsException();
            }

            Like? likeToDelete = await _dbSetLikes.GetLikeAsync(postID, userID, cancellationToken);
            if (likeToDelete is null)
            {
                throw new LikeNotFoundException();
            }
            if(!DataOperations.UserOwnsLike(likeToDelete.UserId, userID))
            {
                throw new UserDoesNotOwnLikeException();
            }
            
            _dbSetLikes.Remove(likeToDelete);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
