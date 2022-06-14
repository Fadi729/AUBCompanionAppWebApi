using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class LikeExtensions
    {
        public static LikeDTO ToLikeDTO(this Like like)
        {
            return new LikeDTO
            {
                UserId    = like.UserId,
                PostId    = like.PostId,
                DateLiked = like.DateLiked
            };
        }
        public static LikeDTOwObjects ToLikeDTOwObjects(this Like like)
        {
            return new LikeDTOwObjects
            {
                User      = like.User.ToProfileQuerryDTO(),
                Post      = like.Post.ToPostDTO(),
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
        public static Like ToLike(this LikeDTO like)
        {
            return new Like
            {
                UserId = like.UserId,
                PostId = like.PostId,
            };
        } 
        public static Like ToLike(this LikePOSTDTO like)
        {
            return new Like
            {
                UserId    = like.UserId,
                PostId    = like.PostId,
                DateLiked = DateTime.Now
            };
        }
    }
}
