using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class LikeExtensions
    {
        public static LikeQueryDTO ToLikeDTO     (this Like like)
        {
            return new LikeQueryDTO
            {
                UserId    = like.UserId,
                PostId    = like.PostId,
                DateLiked = like.DateLiked
            };
        }
        public static LikeDTOUsers ToLikeDTOUsers(this Like like)
        {
            return new LikeDTOUsers
            {
                User      = like.User.ToProfileQuerryDTO(),
                DateLiked = like.DateLiked
            };
        }
    }
}
