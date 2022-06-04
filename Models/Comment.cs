namespace CompanionApp.Models
{
    public partial class Comment
    {
        #region Attributes
        public Guid     PostId      { get; set; }
        public Guid     UserId      { get; set; }
        public string   Text        { get; set; } = null!;
        public Guid     Id          { get; set; }
        public DateTime DateCreated { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Post    Post { get; set; } = null!;
        public virtual Profile User { get; set; } = null!;
        #endregion
    }
}