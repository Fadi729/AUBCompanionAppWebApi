using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostQueryDTO>>                GetPostById             (Guid id)
        {
            return await _postService.GetPostByIdAsync(id);
        }
        
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<PostsByUserDTO>>> GetPostsByUserID        ()
        {
            return Ok(await _postService.GetPostsByUserIDAsync(HttpContext.GetUserID()));
        }

        [HttpGet("user/followings")]
        public async Task<ActionResult<IEnumerable<PostQueryDTO>>>   GetPostsByUserFollowings()
        {
            return Ok(await _postService.GetPostsByUserFollowingsAsync(HttpContext.GetUserID()));
        }

        [HttpPost]
        public async Task<ActionResult<PostPOSTCommandDTO>>          PostPost                (PostPOSTCommandDTO post)
        {
            PostQueryDTO newpost = await _postService.CreatePostAsync(post, HttpContext.GetUserID());
            return CreatedAtAction("GetPostById", new { id = newpost.Id }, newpost);
        }

        [HttpPut("{postID}")]
        public async Task<IActionResult>                             PutPost                 (PostPOSTCommandDTO post, Guid postID)
        {
            await _postService.EditPostAsync(post, postID, HttpContext.GetUserID());
            return NoContent();
        }
        
        [HttpDelete("{postID}")]
        public async Task<IActionResult>                             DeletePost              (Guid postID)
        {
            await _postService.DeletePostAsync(postID, HttpContext.GetUserID());
            return NoContent();
        }
    }
}
