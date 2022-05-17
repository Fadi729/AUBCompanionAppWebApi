using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Post
    {
        public Post()
        {
            CommentPosts = new HashSet<Comment>();
            CommentUsers = new HashSet<Comment>();
            LikeUsers = new HashSet<Like>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Text { get; set; }
        public byte[]? Attachment { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Profile User { get; set; } = null!;
        public virtual Like LikePost { get; set; } = null!;
        public virtual ICollection<Comment> CommentPosts { get; set; }
        public virtual ICollection<Comment> CommentUsers { get; set; }
        public virtual ICollection<Like> LikeUsers { get; set; }
    }
}
