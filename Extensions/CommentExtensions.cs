using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class CommentExtensions
    {
        public static CommentQueryDTO ToCommentQueryDTO (this Comment comment)
        {
            return new CommentQueryDTO
            {
                Id          = comment.Id,
                PostId      = comment.PostId,
                Text        = comment.Text,
                DateCreated = comment.DateCreated,
                User        = comment.User is not null ? comment.User.ToProfileQuerryDTO() : null
            };
        }
        public static Comment         ToComment         (this CommentCommandDTO comment, Guid postID, Guid userID)
        {
            return new Comment
            {
                Id          = Guid.NewGuid(),
                PostId      = postID,
                UserId      = userID,
                Text        = comment.Text,
                DateCreated = DateTime.Now
            };
        }
    }
}
