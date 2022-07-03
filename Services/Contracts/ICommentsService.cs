using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ICommentsService
    {
        public Task<CommentQueryDTO>              GetComment          (Guid commentID);
        public Task<IEnumerable<CommentQueryDTO>> GetPostComments     (Guid postID);
        public Task<int>                          GetPostCommentsCount(Guid postID);
        public Task<CommentQueryDTO>              AddComment          (CommentPOSTCommandDTO comment);
        public Task                               EditComment         (CommentPUTCommandDTO comment);
        public Task                               DeleteComment       (Guid commentID);
    }
}