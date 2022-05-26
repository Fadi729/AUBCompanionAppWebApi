namespace CompanionApp.Models
{
    public partial class Following
    {
        public Guid UserId { get; set; }
        public DateTime? DateFollowed { get; set; }
        public Guid IsFollowing { get; set; }

        public virtual Profile IsFollowingNavigation { get; set; } = null!;
        public virtual Profile User { get; set; } = null!;
    }
}
