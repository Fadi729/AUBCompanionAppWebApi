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
        readonly DbSet<Post> _dbSet;
        public PostService(CompanionAppDBContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Posts;
        }

        public async Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID)
        {
            List<PostsByUserDTO> posts = await _dbSet
                .Where(post => post.UserId == userID)
                .Select(p => p.ToPostsByUserDTO())
                .ToListAsync();
            if (posts == null)
            {
                throw new NoPostsFoundException();
            }
            return posts;
        }
        public async Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowingsAsync(Guid userID)
        {
            IEnumerable<PostQueryDTO> posts = await _dbSet
                .Where(
                    post =>
                        _context.Followings
                            .Where(following => following.UserId == userID)
                            .Select(following => following.IsFollowing)
                            .Contains(post.UserId)
                )
                .Include(post => post.User)
                .Select(post => post.ToPostQueryDTO())
                .ToListAsync();

            if (posts == null)
            {
                throw new NoPostsFoundException();
            }

            return posts;
        }
        public async Task<PostQueryDTO>                GetPostByIdAsync             (Guid id)
        {
            Post? post = await _dbSet.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                throw new PostNotFoundException();
            }

            return post.ToPostQueryDTO();
        }
        public async Task<PostQueryDTO>                CreatePostAsync              (PostCommandDTO post, Guid userID)
        {
            try
            {
                Post newpost = post.ToPost(userID);
                _dbSet.Add(newpost);
                await _context.SaveChangesAsync();
                return newpost.ToPostQueryDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task                              EditPostAsync                (Guid id, Guid userID, PostCommandDTO post)
        {
            try
            {
                if (!await _dbSet.PostExists(id))
                {
                    throw new PostNotFoundException();
                }
                _dbSet.Update(post.ToPost(id, userID));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task                              DeletePostAsync              (Guid id, Guid userId)
        {
            if (!await _dbSet.PostExists(id))
            {
                throw new PostNotFoundException();
            }
            _dbSet.Remove(new Post() { Id = id });
            await _context.SaveChangesAsync();
        }
    }
}
