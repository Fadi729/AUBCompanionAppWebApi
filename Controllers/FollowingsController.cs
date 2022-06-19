using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Exceptions.FollowingsExceptions;

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
            try
            {
                return Ok(await _followingsService.GetIsFollowing(userID));
            }
            catch (Exception ex) when (ex is NoFollowingsFoundException || ex is ProfileNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("followers/{userID}")]
        public async Task<ActionResult<IEnumerable<FollowersDTO>>>   GetFollowers   (Guid userID)
        {
            try
            {
                return Ok(await _followingsService.GetFollowers(userID));
            }
            catch (Exception ex) when (ex is NoFollowingsFoundException || ex is ProfileNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        [HttpPost]
        public async Task<ActionResult<FollowingPOSTDTO>>            PostFollowing  (FollowingPOSTDTO following)
        {
            try
            {
                await _followingsService.Follow(following);
                return following;
            }
            catch (ProfileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FollowingAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpDelete]
        public async Task<IActionResult>                             DeleteFollowing(FollowingPOSTDTO following)
        {
            try
            {
                await _followingsService.Unfollow(following);
                return NoContent();
            }
            catch (Exception ex) when (ex is ProfileNotFoundException || ex is FollowingNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
