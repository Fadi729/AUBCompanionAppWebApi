using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FollowingsController : ControllerBase
    {
        readonly IFollowingsService _followingsService;

        public FollowingsController(IFollowingsService FollowingsService)
        {
            _followingsService = FollowingsService;
        }


        [HttpGet("isfollowing/{userID}")]
        public async Task<ActionResult<IEnumerable<IsFollowingDTO>>> GetFollowing   (Guid userID,           CancellationToken cancellationToken)
        {
            return Ok(await _followingsService.GetIsFollowing(userID, cancellationToken));
        }
        
        
        [HttpGet("followers/{userID}")]
        public async Task<ActionResult<IEnumerable<FollowersDTO>>>   GetFollowers   (Guid userID,           CancellationToken cancellationToken)
        {
            return Ok(await _followingsService.GetFollowers(userID, cancellationToken));
        }


        [HttpPost("{userToFollowID}")]
        public async Task<ActionResult<FollowingPOSTDTO>>            PostFollowing  (Guid userToFollowID,   CancellationToken cancellationToken)
        {
            return Ok(await _followingsService.Follow(HttpContext.GetUserID(), userToFollowID, cancellationToken));
        }


        [HttpDelete("{userToUnfollowID}")]
        public async Task<IActionResult>                             DeleteFollowing(Guid userToUnfollowID, CancellationToken cancellationToken)
        {
            await _followingsService.Unfollow(HttpContext.GetUserID(), userToUnfollowID, cancellationToken);
            return NoContent();
        }
    }
}
