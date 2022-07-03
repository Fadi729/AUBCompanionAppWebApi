namespace CompanionApp.ModelsDTO
{
    public class PostQueryDTO
    {
        public Guid     Id          { get; set; }
        public string?  Text        { get; set; }
        public byte[]?  Attachment  { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ProfileQueryDTO? User        { get; set; } = null!;
    }
    public class PostPOSTCommandDTO
    {
        public string  UserId     { get; set; } = null!;
        public string? Text       { get; set; }
        public byte[]? Attachment { get; set; }
    }
    public class PostPUTCommandDTO
    {
        public string  Id         { get; set; } = null!;
        public string  UserId     { get; set; } = null!;
        public string? Text       { get; set; }
        public byte[]? Attachment { get; set; }
    }
    public class PostsByUserDTO
    {
        public Guid     Id          { get; set; }
        public Guid     UserId      { get; set; }
        public string?  Text        { get; set; }
        public byte[]?  Attachment  { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
