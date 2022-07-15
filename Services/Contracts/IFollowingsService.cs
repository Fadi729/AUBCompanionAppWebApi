using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IFollowingsService
    {
        public Task<IEnumerable<IsFollowingDTO>> GetIsFollowing(Guid userId,                        CancellationToken cancellationToken);
        public Task<IEnumerable<FollowersDTO>>   GetFollowers  (Guid userId,                        CancellationToken cancellationToken);
        public Task<FollowingPOSTDTO>            Follow        (Guid userID, Guid userToFollowID,   CancellationToken cancellationToken);
        public Task                              Unfollow      (Guid userID, Guid userToUnfollowID, CancellationToken cancellationToken);
    }
}
