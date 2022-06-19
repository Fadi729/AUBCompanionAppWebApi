using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.PostExceptions;

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
            try
            {
                return await _postService.GetPostByIdAsync(id);
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<PostsByUserDTO>>> GetPostsByUserID        (Guid userID)
        {
            try
            {
                return Ok(await _postService.GetPostsByUserIDAsync(userID));
            }
            catch (NoPostsFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("user/followings/{userID}")]
        public async Task<ActionResult<IEnumerable<PostQueryDTO>>>   GetPostsByUserFollowings(Guid userID)
        {
            try
            {
                return Ok(await _postService.GetPostsByUserFollowingsAsync(userID));
            }
            catch (NoPostsFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("{userID}")]
        public async Task<ActionResult<Post>>                        PostPost                (PostCommandDTO post, Guid userID)
        {
            try
            {
                PostQueryDTO newpost = await _postService.CreatePostAsync(post, userID);
                return CreatedAtAction("GetPostById", new { id = newpost.Id }, newpost);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{userID}/{id}")]
        public async Task<IActionResult>                             PutPost                 (Guid id, Guid userID, PostCommandDTO post)
        {
            try
            {
                await _postService.EditPostAsync(id, userID, post);
                return NoContent();
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{userID}/{id}")]
        public async Task<IActionResult>                             DeletePost              (Guid id, Guid userID)
        {
            try
            {
                await _postService.DeletePostAsync(id, userID);
                return NoContent();
            }
            catch (PostNotFoundException ex)
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
