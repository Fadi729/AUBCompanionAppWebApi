using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class CommentExtensions
    {
        public static CommentDTO ToCommentDTO (this Comment comment)
        {
            return new CommentDTO
            {
                Id          = comment.Id,
                PostId      = comment.PostId,
                Text        = comment.Text,
                DateCreated = comment.DateCreated,
                User        = comment.User.ToProfileDTO()
            };
        }
        public static Comment ToComment(this CommentPOSTDTO comment)
        {
            return new Comment
            {
                Id          = Guid.NewGuid(),
                PostId      = comment.PostId,
                UserId      = comment.UserId,
                Text        = comment.Text,
                DateCreated = DateTime.Now
            };
        }
    }
}
