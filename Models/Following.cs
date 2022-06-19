namespace CompanionApp.Models
{
    public partial class Following
    {
        #region Attributes
        public Guid      UserId       { get; set; }
        public DateTime? DateFollowed { get; set; }
        public Guid      IsFollowing  { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Profile User                  { get; set; } = null!;
        public virtual Profile IsFollowingNavigation { get; set; } = null!;
        #endregion
    }
}