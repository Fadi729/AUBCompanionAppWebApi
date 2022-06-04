namespace CompanionApp.Models
{
    public partial class Like
    {
        #region Attributes
        public Guid     PostId    { get; set; }
        public DateTime DateLiked { get; set; }
        public Guid     UserId    { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Post    Post { get; set; } = null!;
        public virtual Profile User { get; set; } = null!;
        #endregion
    }
}
