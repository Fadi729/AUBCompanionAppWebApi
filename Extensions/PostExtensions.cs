using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class PostExtensions
    {
        public static Post           ToPost          (this PostPOSTCommandDTO post)
        {
            return new Post
            {
                Id          = Guid.NewGuid(),
                UserId      = Guid.Parse(post.UserId),
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = DateTime.Now
            };
        }
        public static Post           ToPost          (this PostPOSTCommandDTO post, Guid Id)
        {
            return new Post
            {
                Id          = Id,
                UserId      = Guid.Parse(post.UserId),
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = DateTime.Now
            };
        }
        public static Post           ToPost          (this PostPUTCommandDTO post)
        {
            return new Post
            {
                Id          = Guid.Parse(post.Id),
                UserId      = Guid.Parse(post.UserId),
                Text        = post.Text,
                Attachment  = post.Attachment,
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
