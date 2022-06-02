namespace CompanionApp.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes    = new HashSet<Like>();
        }

        public Guid     Id          { get; set; }
        public Guid     UserId      { get; set; }
        public string?  Text        { get; set; }
        public byte[]?  Attachment  { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Profile              User     { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like>    Likes    { get; set; }
    }
}
