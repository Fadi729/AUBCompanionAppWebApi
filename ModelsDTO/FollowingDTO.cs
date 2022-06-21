namespace CompanionApp.ModelsDTO
{
    public class IsFollowingDTO
    {
        public DateTime?          DateFollowed          { get; set; }
        public virtual ProfileQueryDTO? IsFollowingNavigation { get; set; } = null!;
    }

    public class FollowersDTO
    {
        public DateTime?          DateFollowed          { get; set; }
        public virtual ProfileQueryDTO User { get; set; } = null!;
    }
    
    public class FollowingPOSTDTO
    {
        public string UserId      { get; set; } = null!;
        public string IsFollowing { get; set; } = null!;
    }
}
