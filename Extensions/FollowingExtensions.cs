using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class FollowingExtensions
    {
        public static IsFollowingDTO ToIsFollowingDTO(this Following following)
        {
            return new IsFollowingDTO
            {
                DateFollowed            = following.DateFollowed,
                IsFollowingNavigation   = following.IsFollowingNavigation is not null ? following.IsFollowingNavigation.ToProfileQuerryDTO() : null
            };
        } 
        
        public static FollowersDTO   ToFollowersDTO  (this Following following)
        {
            return new FollowersDTO
            {
                DateFollowed          = following.DateFollowed,
                User                  = following.User.ToProfileQuerryDTO()
            };
        } 

        public static Following      ToFollowing     (this FollowingPOSTDTO following)
        {
            return new Following
            {
                UserId       = following.UserId,
                IsFollowing  = following.IsFollowing,
                DateFollowed = DateTime.Now,
            };
        }
    }
}
