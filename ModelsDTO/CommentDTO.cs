namespace CompanionApp.ModelsDTO
{
    public class CommentQueryDTO
    {
        public Guid               Id          { get; set; }
        public Guid               PostId      { get; set; }
        public string             Text        { get; set; } = null!;
        public DateTime           DateCreated { get; set; }
        public virtual ProfileQueryDTO? User        { get; set; } = null!;
    }
    public class CommentCommandDTO
    {
        public string Text   { get; set; } = null!;
    }
}
