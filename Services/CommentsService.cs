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

        public async Task<CommentQueryDTO>              GetComment          (Guid commentID, CancellationToken cancellationToken)
        {
            Comment? comment = await _dbSetComment
                .Include            (comment => comment.User)
                .FirstOrDefaultAsync(comment => comment.Id == commentID, cancellationToken);
            if (comment == null)
            {
                throw new CommentNotFoundException();
            }
            return comment.ToCommentQueryDTO();
        }
        public async Task<IEnumerable<CommentQueryDTO>> GetPostComments     (Guid postID,    CancellationToken cancellationToken)
        {
            if (!await _dbSetPost.PostExists(postID, cancellationToken))
            {
                throw new PostNotFoundException();
            }

            IEnumerable<CommentQueryDTO> comments = await _dbSetComment
                .Include(comment => comment.User)
                .Where  (comment => comment.PostId == postID)
                .Select (comment => comment.ToCommentQueryDTO())
                .ToListAsync(cancellationToken);

            if (!comments.Any())
            {
                throw new NoCommentsFoundException();
            }
            return comments;
        }
        public async Task<int>                          GetPostCommentsCount(Guid postID,    CancellationToken cancellationToken)
        {
            return await _dbSetComment
                .Where(comment => comment.PostId == postID)
                .CountAsync(cancellationToken);
        }
        public async Task<CommentQueryDTO>              AddComment          (CommentPOSTCommandDTO comment, Guid postID, Guid userID, CancellationToken cancellationToken)
        {
            if (!await _dbSetPost.PostExists(postID, cancellationToken))
            {
                throw new PostNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }

            Comment newComment = comment.ToComment(postID, userID);
            _dbSetComment.Add(newComment);
            await _context.SaveChangesAsync(cancellationToken);
            return newComment.ToCommentQueryDTO();
        }
        public async Task                               EditComment         (CommentPOSTCommandDTO comment, Guid commentID, Guid postID, Guid userID, CancellationToken cancellationToken)
        {
            if (!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetPost.PostExists(postID, cancellationToken))
            {
                throw new PostNotFoundException();
            }
            
            Comment? commentToEdit = await _dbSetComment.GetCommentAsync(commentID, cancellationToken);
            if (commentToEdit is null)
            {
                throw new CommentNotFoundException();
            }

            if(!DataOperations.CommentBelongsToPost(commentToEdit.PostId, postID))
            {
                throw new CommentDoesNotBelongToPostException();
            }
            if(!DataOperations.UserOwnsComment(commentToEdit.UserId, userID))
            {
                throw new UserDoesNotOwnCommentException();
            }

            _dbSetComment.Update(comment.ToComment(commentID, postID, userID));
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task                               DeleteComment       (Guid commentID, Guid userID, CancellationToken cancellationToken)
        {
            if(!await _dbSetProfile.ProfileExists(userID, cancellationToken))
            {
                throw new ProfileNotFoundException();
            }

            Comment? commentToDelete = await _dbSetComment.GetCommentAsync(commentID, cancellationToken);
            if(commentToDelete is null)
            {
                throw new CommentNotFoundException();
            }
            if(!DataOperations.UserOwnsComment(commentToDelete.UserId, userID))
            {
                throw new UserDoesNotOwnCommentException();
            }

            _dbSetComment.Remove(commentToDelete);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}