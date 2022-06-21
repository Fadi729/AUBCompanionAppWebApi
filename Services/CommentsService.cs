using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Exceptions.CommentExceptions;
using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Validation.CommentValidation;

namespace CompanionApp.Services
{
    public class CommentsService : ICommentsService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Comment>        _dbSetComment;
        readonly DbSet<Post>           _dbSetPost;
        readonly DbSet<Profile>        _dbSetProfile;
        readonly AddCommentValidation  _addCommentValidator;
        readonly EditCommentValidation _editCommentValidator;

        public CommentsService(CompanionAppDBContext context, 
            AddCommentValidation  addValidator, 
            EditCommentValidation editValidator)
        {
            _context              = context;
            _dbSetComment         = context.Comments;
            _dbSetPost            = context.Posts;
            _dbSetProfile         = context.Profiles;
            _addCommentValidator  = addValidator;
            _editCommentValidator = editValidator;
        }

        public async Task<CommentQueryDTO>              GetComment          (Guid commentID)
        {
            Comment? comment = await _dbSetComment
                .Include            (comment => comment.User)
                .FirstOrDefaultAsync(comment => comment.Id == commentID);
            if (comment == null)
            {
                throw new CommentNotFoundException();
            }
            return comment.ToCommentQueryDTO();
        }
        public async Task<IEnumerable<CommentQueryDTO>> GetPostComments     (Guid postID)
        {
            if (!await _dbSetPost.PostExists(postID.ToString()))
            {
                throw new PostNotFoundException();
            }

            IEnumerable<CommentQueryDTO> comments = await _dbSetComment
                .Include(comment => comment.User)
                .Where  (comment => comment.PostId == postID)
                .Select (comment => comment.ToCommentQueryDTO())
                .ToListAsync();

            if (!comments.Any())
            {
                throw new NoCommentsFoundException();
            }
            return comments;
        }
        public async Task<int>                          GetPostCommentsCount(Guid postID)
        {
            return (await GetPostComments(postID)).Count();
        }
        public async Task<CommentQueryDTO>              AddComment          (CommentPOSTCommandDTO comment)
        {
            await _addCommentValidator.ValidateAndThrowAsync(comment);
            if (!await _dbSetPost.PostExists(comment.PostID.ToString()))
            {
                throw new PostNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(Guid.Parse(comment.UserID)))
            {
                throw new ProfileNotFoundException();
            }

            Comment newComment = comment.ToComment();
            _dbSetComment.Add(newComment);
            await _context.SaveChangesAsync();
            return newComment.ToCommentQueryDTO();
        }
        public async Task                               EditComment         (CommentPUTCommandDTO comment)
        {
            await _editCommentValidator.ValidateAndThrowAsync(comment);
            if (!await _dbSetComment.CommentExists(comment.Id))
            {
                throw new CommentNotFoundException();
            }
            if (!await _dbSetPost.PostExists(comment.PostID))
            {
                throw new PostNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(comment.UserID))
            {
                throw new ProfileNotFoundException();
            }

            _dbSetComment.Update(comment.ToComment());
            await _context.SaveChangesAsync();
        }
        public async Task                               DeleteComment       (Guid commentID)
        {
            if(!await _dbSetComment.CommentExists(commentID.ToString()))
            {
                throw new CommentNotFoundException();
            }

            _dbSetComment.Remove(new Comment { Id = commentID });
            await _context.SaveChangesAsync();
        }
    }
}