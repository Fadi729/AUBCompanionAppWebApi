using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostsByUserDTO>> GetPostsByUserID        (Guid userID);
        public Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowings(Guid userID);
        public Task<PostQueryDTO>                GetPostById             (Guid id);
        public Task<PostQueryDTO>                CreatePost              (PostCommandDTO post, Guid userID);
        public Task                              EditPost                (Guid id, Guid userID, PostCommandDTO post);
        public Task                              DeletePost              (Guid id, Guid userId);
    }
}
