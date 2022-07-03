using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LikesController : ControllerBase
    {
        readonly ILikesService _likeService;

        public LikesController(ILikesService likeService)
        {
            _likeService = likeService;
        }


        [HttpGet("post/{postID}")]
        public async Task<ActionResult<IEnumerable<LikeDTOUsers>>> GetPostLikes       (Guid postID)
        {
            return Ok(await _likeService.GetPostLikes(postID));

        }

        [HttpGet("counter/{postID}")]
        public async Task<ActionResult<int>>                       GetPostLikesCounter(Guid postID)
        {
            return Ok(await _likeService.GetPostLikesCount(postID));
        }

        [HttpPost("post/{postID}")]
        public async Task<ActionResult<LikeQueryDTO>>              PostLike           (Guid postID)
        {
            return await _likeService.LikePost(postID, HttpContext.GetUserID());
        }

        [HttpDelete("post/{postID}")]
        public async Task<IActionResult>                           DeleteLike         (Guid postID)
        {
            await _likeService.UnlikePost(postID, HttpContext.GetUserID());
            return NoContent();
        }
    }
}
