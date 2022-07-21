using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ILikesService
    {
        public Task<IEnumerable<LikeDTOUsers>> GetPostLikes     (Guid postID,              CancellationToken cancellationToken);
        public Task<int>                       GetPostLikesCount(Guid postID,              CancellationToken cancellationToken);
        public Task<LikeQueryDTO>              LikePost         (Guid postID, Guid userID, CancellationToken cancellationToken);
        public Task                            UnlikePost       (Guid postID, Guid userID, CancellationToken cancellationToken);
    }
}
