﻿using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface IPostService
    {
        public Task<IEnumerable<PostsByUserDTO>> GetPostsByUserIDAsync        (Guid userID);
        public Task<IEnumerable<PostQueryDTO>>   GetPostsByUserFollowingsAsync(Guid userID);
        public Task<PostQueryDTO>                GetPostByIdAsync             (Guid id);
        public Task<PostQueryDTO>                CreatePostAsync              (PostCommandDTO post, Guid userID);
        public Task                              EditPostAsync                (Guid id, Guid userID, PostCommandDTO post);
        public Task                              DeletePostAsync              (Guid id, Guid userId);
    }
}
