using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        
        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<PostsByUserDTO>>> GetPostsByUserID        (Guid userID)
        {
            return Ok(await _postService.GetPostsByUserIDAsync(userID));
        }

        [HttpGet("user/followings/{userID}")]
        public async Task<ActionResult<IEnumerable<PostQueryDTO>>>   GetPostsByUserFollowings(Guid userID)
        {
            return Ok(await _postService.GetPostsByUserFollowingsAsync(userID));
        }

        [HttpPost("{userID}")]
        public async Task<ActionResult<Post>>                        PostPost                (PostCommandDTO post, Guid userID)
        {
            PostQueryDTO newpost = await _postService.CreatePostAsync(post, userID);
            return CreatedAtAction("GetPostById", new { id = newpost.Id }, newpost);
        }

        [HttpPut("{userID}/{id}")]
        public async Task<IActionResult>                             PutPost                 (Guid id, Guid userID, PostCommandDTO post)
        {
            await _postService.EditPostAsync(id, userID, post);
            return NoContent();
        }

        [HttpDelete("{userID}/{id}")]
        public async Task<IActionResult>                             DeletePost              (Guid id, Guid userID)
        {
            await _postService.DeletePostAsync(id, userID);
            return NoContent();
        }
    }
}
