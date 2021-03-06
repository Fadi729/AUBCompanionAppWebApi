using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.PostExceptions;

namespace CompanionApp.Services
{
    public class PostService : IPostService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Post>           _dbSet;
        public PostService(CompanionAppDBContext dbContext)
        {
            _context = dbContext;
            _dbSet   = _context.Posts;
        }

        public async Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID, CancellationToken cancellationToken)
        {
            IEnumerable<PostsByUserDTO> posts = await _dbSet
                .Where (post => post.UserId == userID)
                .Select(p => p.ToPostsByUserDTO())
                .ToListAsync(cancellationToken);
            if (!posts.Any())
            {
                throw new NoPostsFoundException();
            }
            return posts;
        }
        public async Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowingsAsync(Guid userID, CancellationToken cancellationToken)
        {
            IEnumerable<PostQueryDTO> posts = await _dbSet
                .Where(
                    post =>
                        _context.Followings
                            .Where   (following => following.UserId == userID)
                            .Select  (following => following.IsFollowing)
                            .Contains(post.UserId)
                )
                .Include    (post => post.User)
                .Select     (post => post.ToPostQueryDTO())
                .ToListAsync(cancellationToken);

            if (!posts.Any())
            {
                throw new NoPostsFoundException();
            }

            return posts;
        }
        public async Task<PostQueryDTO>                GetPostByIdAsync             (Guid id,     CancellationToken cancellationToken)
        {
            Post? post = await _dbSet
                .Include            (p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (post == null)
            {
                throw new PostNotFoundException();
            }

            return post.ToPostQueryDTO();
        }
        public async Task<PostQueryDTO>                CreatePostAsync              (PostPOSTCommandDTO post, Guid userID,              CancellationToken cancellationToken)
        {
            try
            {
                Post newpost = post.ToPost(userID);
                _dbSet.Add(newpost);
                await _context.SaveChangesAsync(cancellationToken);
                return newpost.ToPostQueryDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task                              EditPostAsync                (PostPOSTCommandDTO post, Guid postID, Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                Post? postToEdit = await _dbSet.GetPostAsync(postID, cancellationToken);
                
                if (postToEdit is null)
                {
                    throw new PostNotFoundException();
                }
                if (!DataOperations.UserOwnsPost(postToEdit.UserId, userId))
                {
                    throw new UserDoesNotOwnPostException();
                }
                _dbSet.Update(post.ToPost(postID, userId));
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task                              DeletePostAsync              (Guid postID, Guid userID,                          CancellationToken cancellationToken)
        {
            Post? postToDelete = await _dbSet.GetPostAsync(postID, cancellationToken);

            if (postToDelete is null)
            {
                throw new PostNotFoundException();
            }           
            if (!DataOperations.UserOwnsPost(postToDelete.UserId, userID))
            {
                throw new UnauthorizedAccessException();
            }
            
            _dbSet.Remove(postToDelete);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}