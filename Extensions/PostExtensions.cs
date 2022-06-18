using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class PostExtensions
    {
        public static Post           ToPost          (this PostCommandDTO post, Guid userId)
        {
            return new Post
            {
                Id          = Guid.NewGuid(),
                UserId      = userId,
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = DateTime.Now
            };
        }
        public static Post           ToPost          (this PostCommandDTO post, Guid Id, Guid userID)
        {
            return new Post
            {
                Id          = Id,
                UserId      = userID,
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = DateTime.Now
            };
        }
        public static PostQueryDTO   ToPostQueryDTO  (this Post post)
        {
            return new PostQueryDTO
            {
                Id          = post.Id,
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = post.DateCreated,
                User        = post.User is not null ? post.User.ToProfileQuerryDTO() : null
            };
        }
        public static PostsByUserDTO ToPostsByUserDTO(this Post post)
        {
            return new PostsByUserDTO
            {
                Id          = post.Id,
                UserId      = post.UserId,
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = post.DateCreated,
            };
        }
    }
}
