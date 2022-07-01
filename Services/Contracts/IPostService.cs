using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID);
        public Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowingsAsync(Guid userID);
        public Task<PostQueryDTO>                GetPostByIdAsync             (Guid id);
        public Task<PostQueryDTO>                CreatePostAsync              (PostPOSTCommandDTO post, string userID);
        public Task                              EditPostAsync                (PostPOSTCommandDTO post, string postID, string userID);
        public Task                              DeletePostAsync              (string id, string userId);
    }
}
