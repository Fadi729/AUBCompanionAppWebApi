using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingsController : ControllerBase
    {
        readonly IFollowingsService _followingsService;

        public FollowingsController(IFollowingsService FollowingsService)
        {
            _followingsService = FollowingsService;
        }

        
        [HttpGet("isfollowing/{userID}")]
        public async Task<ActionResult<IEnumerable<IsFollowingDTO>>> GetFollowing   (Guid userID)
        {
            return Ok(await _followingsService.GetIsFollowing(userID));
        }


        [HttpGet("followers/{userID}")]
        public async Task<ActionResult<IEnumerable<FollowersDTO>>>   GetFollowers   (Guid userID)
        {
            return Ok(await _followingsService.GetFollowers(userID));
        }
        

        [HttpPost]
        public async Task<ActionResult<FollowingPOSTDTO>>            PostFollowing  (FollowingPOSTDTO following)
        {
            await _followingsService.Follow(following);
            return following;
        }

        
        [HttpDelete]
        public async Task<IActionResult>                             DeleteFollowing(FollowingPOSTDTO following)
        {
            await _followingsService.Unfollow(following);
            return NoContent();
        }
    }
}
