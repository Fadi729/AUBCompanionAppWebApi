using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class PostExtensions
    {
        public static PostDTO ToPostDTO(this Post post)
        {
            return new PostDTO
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
        public static Post ToPost(this PostPOSTDTO post)
        {
            return new Post
            {
                Id          = Guid.NewGuid(),
                UserId      = post.UserId,
                Text        = post.Text,
                Attachment  = post.Attachment,
                DateCreated = DateTime.Now
            };
        }
        
    }
}
