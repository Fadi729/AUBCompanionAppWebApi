using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Validation.PostValidation;

namespace CompanionApp.Services
{
    public class PostService : IPostService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Post>           _dbSet;
        readonly AddPostValidation     _addPostValidator;
        readonly EditPostValidation    _editPostValidator;
        public PostService(CompanionAppDBContext dbContext, AddPostValidation PostValidator, EditPostValidation EditPostValidator)
        {
            _context           = dbContext;
            _dbSet             = _context.Posts;
            _addPostValidator  = PostValidator;
            _editPostValidator = EditPostValidator;
        }

        public async Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID)
        {
            List<PostsByUserDTO> posts = await _dbSet
                .Where (post => post.UserId == userID)
                .Select(p => p.ToPostsByUserDTO())
                .ToListAsync();
            if (!posts.Any())
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
                            .Where   (following => following.UserId == userID)
                            .Select  (following => following.IsFollowing)
                            .Contains(post.UserId)
                )
                .Include(post => post.User)
                .Select (post => post.ToPostQueryDTO())
                .ToListAsync();

            if (!posts.Any())
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
        public async Task<PostQueryDTO>                CreatePostAsync              (PostPOSTCommandDTO post, string userID)
        {
            try
            {
                //await _addPostValidator.ValidateAndThrowAsync(post);
                Post newpost = post.ToPost(Guid.Parse(userID));
                _dbSet.Add(newpost);
                await _context.SaveChangesAsync();
                return newpost.ToPostQueryDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task                              EditPostAsync                (PostPOSTCommandDTO post, string postID, string userId)
        {
            try
            {
                Post? postToEdit = await _dbSet.GetPost(postID);

                if (postToEdit is null)
                {
                    throw new PostNotFoundException();
                }
                if (!DataOperations.UserOwnsPost(postToEdit.UserId.ToString(), userId))
                {
                    throw new UserDoesNotOwnPostException();
                }
                
                //await _editPostValidator.ValidateAndThrowAsync(post);
                _dbSet.Update(post.ToPost(Guid.Parse(postID), Guid.Parse(userId)));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task                              DeletePostAsync              (string postID, string userID)
        {
            Post? postToDelete = await _dbSet.GetPost(postID.ToString());

            if (postToDelete is null)
            {
                throw new PostNotFoundException();
            }           
            if (!DataOperations.UserOwnsPost(postToDelete.UserId.ToString(), userID.ToString()))
            {
                throw new UnauthorizedAccessException();
            }
            
            _dbSet.Remove(new Post() { Id = Guid.Parse(postID) });
            await _context.SaveChangesAsync();
        }
    }
}