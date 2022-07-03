using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID);
        public Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowingsAsync(Guid userID);
        public Task<PostQueryDTO>                GetPostByIdAsync             (Guid id);
        public Task<PostQueryDTO>                CreatePostAsync              (PostPOSTCommandDTO post, Guid userID);
        public Task                              EditPostAsync                (PostPOSTCommandDTO post, Guid postID, Guid userID);
        public Task                              DeletePostAsync              (Guid postID, Guid userID);
    }
}
