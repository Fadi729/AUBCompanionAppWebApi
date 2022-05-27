namespace CompanionApp.Models
{
    public partial class Like
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateLiked { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Profile User { get; set; } = null!;
    }
}
