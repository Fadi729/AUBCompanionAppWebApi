using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID                ,                           CancellationToken cancellationToken);
        public Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowingsAsync(Guid userID                ,                           CancellationToken cancellationToken);
        public Task<PostQueryDTO>                GetPostByIdAsync             (Guid id                    ,                           CancellationToken cancellationToken);
        public Task<PostQueryDTO>                CreatePostAsync              (PostPOSTCommandDTO post    , Guid userID,              CancellationToken cancellationToken);
        public Task                              EditPostAsync                (PostPOSTCommandDTO post    , Guid postID, Guid userID, CancellationToken cancellationToken);
        public Task                              DeletePostAsync              (Guid postID                , Guid userID,              CancellationToken cancellationToken);
    }
}
