using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        readonly ILikesService _likeService;

        public LikesController(ILikesService likeService)
        {
            _likeService = likeService;
        }


        [HttpGet("{postID}")]
        public async Task<ActionResult<IEnumerable<LikeDTOUsers>>> GetPostLikes       (Guid postID)
        {
            return Ok(await _likeService.GetPostLikes(postID));

        }

        [HttpGet("counter/{postID}")]
        public async Task<ActionResult<int>>                       GetPostLikesCounter(Guid postID)
        {
            return Ok(await _likeService.GetPostLikesCount(postID));
        }

        [HttpPost]
        public async Task<ActionResult<LikeDTO>>                   PostLike           (LikePOSTDTO like)
        {
            return await _likeService.LikePost(like);;
        }

        [HttpDelete("{postID}/{userID}")]
        public async Task<IActionResult>                           DeleteLike         (Guid postID, Guid userID)
        {
            await _likeService.UnlikePost(postID, userID);
            return NoContent();
        }
    }
}
