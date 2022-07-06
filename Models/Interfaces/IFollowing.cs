namespace CompanionApp.Models.Interfaces;

public interface IFollowing
{
    Guid UserId { get; set; }
    DateTime? DateFollowed { get; set; }
    Guid IsFollowing { get; set; }
    Profile User { get; set; }
    Profile IsFollowingNavigation { get; set; }
}