namespace CompanionApp.ModelsDTO
{
    public class PostDTO
    {
        public Guid                Id          { get; set; }
        public string?             Text        { get; set; }
        public byte[]?             Attachment  { get; set; }
        public DateTime            DateCreated { get; set; }

        public virtual ProfileQueryDTO? User        { get; set; } = null!;
    }
    public class PostPOSTDTO
    {
        public Guid    UserId     { get; set; }
        public string? Text       { get; set; }
        public byte[]? Attachment { get; set; }
    }
    public class PostPUTDTO
    {
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
