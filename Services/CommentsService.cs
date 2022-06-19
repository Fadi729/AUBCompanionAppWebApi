using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Exceptions.CommentExceptions;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Services
{
    public class CommentsService : ICommentsService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Comment>        _dbSetComment;
        readonly DbSet<Post>           _dbSetPost;
        readonly DbSet<Profile>        _dbSetProfile;

        public CommentsService(CompanionAppDBContext context)
        {
            _context      = context;
            _dbSetComment = context.Comments;
            _dbSetPost    = context.Posts;
            _dbSetProfile = context.Profiles;
        }

        public async Task<CommentQueryDTO>              GetComment          (Guid commentID)
        {
            Comment? comment = await _dbSetComment
                .Include(comment => comment.User)
                .FirstOrDefaultAsync(comment => comment.Id == commentID);
            if (comment == null)
            {
                throw new CommentNotFoundException();
            }
            return comment.ToCommentQueryDTO();
        }
        public async Task<IEnumerable<CommentQueryDTO>> GetPostComments     (Guid postID)
        {
            if (!await _dbSetPost.PostExists(postID))
            {
                throw new PostNotFoundException();
            }

            IEnumerable<CommentQueryDTO> comments = await _dbSetComment
                .Include(comment => comment.User)
                .Where(comment => comment.PostId == postID)
                .Select(comment => comment.ToCommentQueryDTO())
                .ToListAsync();

            if (comments is null)
            {
                throw new NoCommentsFoundException();
            }
            return comments;
        }
        public async Task<int>                          GetPostCommentsCount(Guid postID)
        {
            return (await GetPostComments(postID)).Count();
        }
        public async Task<CommentQueryDTO>              AddComment          (Guid postID, Guid userID, CommentCommandDTO comment)
        {
            if (!await _dbSetPost.PostExists(postID))
            {
                throw new PostNotFoundException();
            }

            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }

            Comment newComment = comment.ToComment(postID, userID);
            _dbSetComment.Add(newComment);
            await _context.SaveChangesAsync();
            return newComment.ToCommentQueryDTO();
        }
        public async Task                               EditComment         (Guid commentID, Guid postID, Guid userID, CommentCommandDTO comment)
        {
            if (!await _dbSetComment.CommentExists(commentID))
            {
                throw new CommentNotFoundException();
            }
            if (!await _dbSetPost.PostExists(postID))
            {
                throw new PostNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }

            _dbSetComment.Update(comment.ToComment(postID, userID));
            await _context.SaveChangesAsync();
        }
        public async Task                               DeleteComment       (Guid commentID)
        {
            if(!await _dbSetComment.CommentExists(commentID))
            {
                throw new CommentNotFoundException();
            }

            _dbSetComment.Remove(new Comment { Id = commentID });
            await _context.SaveChangesAsync();
        }
    }
}
