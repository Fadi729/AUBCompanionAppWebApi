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
        readonly CompanionAppDBContext _context;
        readonly IPostService          _postService;

        public PostsController(CompanionAppDBContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostQueryDTO>>                GetPostById             (Guid id)
        {
            try
            {
                return await _postService.GetPostById(id);
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
                return Ok(await _postService.GetPostsByUserID(userID));
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
                return Ok(await _postService.GetPostsByUserFollowings(userID));
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
                PostQueryDTO newpost = await _postService.CreatePost(post, userID);
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
                await _postService.EditPost(id, userID, post);
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
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            if (post.UserId != userID)
            {
                return Unauthorized();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
