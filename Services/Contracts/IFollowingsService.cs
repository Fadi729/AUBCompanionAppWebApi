using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IFollowingsService
    {
        public Task<IEnumerable<IsFollowingDTO>> GetIsFollowing(Guid userId);
        public Task<IEnumerable<FollowersDTO>>   GetFollowers  (Guid userId);
        public Task                              Follow        (FollowingPOSTDTO following);
        public Task                              Unfollow      (FollowingPOSTDTO following);
    }
}
