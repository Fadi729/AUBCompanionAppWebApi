namespace CompanionApp.Models.Interfaces;

public interface ILike
{
    Guid PostId { get; set; }
    DateTime DateLiked { get; set; }
    Guid UserId { get; set; }
    Post Post { get; set; }
    Profile User { get; set; }
}