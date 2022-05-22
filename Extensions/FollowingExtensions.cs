using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class FollowingExtensions
    {
        public static FollowingDTO ToFollowingDTO(this Following following)
        {
            return new FollowingDTO
            {
                DateFollowed = following.DateFollowed,
                IsFollowingNavigation = following.IsFollowingNavigation.ToProfileDTO()
            };
        }
        
        public static Following ToFollowing(this FollowingPOSTDTO following)
        {
            return new Following
            {
                UserId = following.UserId,
                IsFollowing = following.IsFollowing,
                DateFollowed = DateTime.Now,
            };
        }
    }
}
