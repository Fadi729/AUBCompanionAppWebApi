namespace CompanionApp.ModelsDTO
{
    public class CommentDTO
    {
        public Guid               Id          { get; set; }
        public Guid               PostId      { get; set; }
        public string             Text        { get; set; } = null!;
        public DateTime           DateCreated { get; set; }
        public virtual ProfileQuerryDTO User        { get; set; } = null!;
    }
    public class CommentPOSTDTO
    {
        public Guid   UserId { get; set; }
        public Guid   PostId { get; set; }
        public string Text   { get; set; } = null!;
    }
    
    public class CommentPUTDTO
    {
        public string Text { get; set; } = null!;
    }
}
