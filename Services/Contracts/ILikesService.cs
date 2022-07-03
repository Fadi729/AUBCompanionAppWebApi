using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ILikesService
    {
        public Task<IEnumerable<LikeDTOUsers>> GetPostLikes     (Guid postID);
        public Task<int>                       GetPostLikesCount(Guid postID);
        public Task<LikeDTO>                   LikePost         (LikePOSTDTO like);
        public Task                            UnlikePost       (LikePOSTDTO like);
    }
}
