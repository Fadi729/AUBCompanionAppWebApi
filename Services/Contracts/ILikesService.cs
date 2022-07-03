using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ILikesService
    {
        public Task<IEnumerable<LikeDTOUsers>> GetPostLikes     (Guid postID);
        public Task<int>                       GetPostLikesCount(Guid postID);
        public Task<LikeQueryDTO>              LikePost         (Guid postID, Guid userID);
        public Task                            UnlikePost       (Guid postID, Guid userID);
    }
}
