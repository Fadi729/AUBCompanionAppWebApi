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
        public static Comment         ToComment         (this CommentPOSTCommandDTO comment)
        {
            return new Comment
            {
                Id          = Guid.NewGuid(),
                PostId      = Guid.Parse(comment.PostID),
                UserId      = Guid.Parse(comment.UserID),
                Text        = comment.Text,
                DateCreated = DateTime.Now
            };
        }
        public static Comment         ToComment         (this CommentPUTCommandDTO comment)
        {
            return new Comment
            {
                Id          = Guid.Parse(comment.Id),
                PostId      = Guid.Parse(comment.PostID),
                UserId      = Guid.Parse(comment.UserID),
                Text        = comment.Text,
                DateCreated = DateTime.Now
            };
        }
    }
}
