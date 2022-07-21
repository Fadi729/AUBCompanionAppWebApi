using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ICommentsService
    {
        public Task<CommentQueryDTO>              GetComment          (Guid commentID, CancellationToken cancellationToken);
        public Task<IEnumerable<CommentQueryDTO>> GetPostComments     (Guid postID, CancellationToken cancellationToken);
        public Task<int>                          GetPostCommentsCount(Guid postID, CancellationToken cancellationToken);
        public Task<CommentQueryDTO>              AddComment          (CommentPOSTCommandDTO comment, Guid postID, Guid userID, CancellationToken cancellationToken);
        public Task                               EditComment         (CommentPOSTCommandDTO comment, Guid commentID, Guid postID, Guid userID, CancellationToken cancellationToken);
        public Task                               DeleteComment       (Guid commentID, Guid userID, CancellationToken cancellationToken);
    }
}