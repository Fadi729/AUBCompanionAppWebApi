using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IFollowingsService
    {
        public Task<IEnumerable<IsFollowingDTO>> GetIsFollowing(Guid userId);
        public Task<IEnumerable<FollowersDTO>>   GetFollowers  (Guid userId);
        public Task<FollowingPOSTDTO>            Follow        (Guid userID, Guid userToFollowID);
        public Task                              Unfollow      (Guid userID, Guid userToUnfollowID);
    }
}
