namespace CompanionApp.Models.Interfaces;

public interface IComment
{
    Guid PostId { get; set; }
    Guid UserId { get; set; }
    string Text { get; set; }
    Guid Id { get; set; }
    DateTime DateCreated { get; set; }
    Post Post { get; set; }
    Profile User { get; set; }
}