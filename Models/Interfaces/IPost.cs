namespace CompanionApp.Models.Interfaces;

public interface IPost
{
    Guid Id { get; set; }
    Guid UserId { get; set; }
    string? Text { get; set; }
    byte[]? Attachment { get; set; }
    DateTime DateCreated { get; set; }
    Profile User { get; set; }
    ICollection<Comment> Comments { get; set; }
    ICollection<Like> Likes { get; set; }
}