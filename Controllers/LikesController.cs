using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
         readonly CompanionAppDBContext _context;

        public LikesController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Likes/5
        [HttpGet("{postID}/{userID}")]
        public async Task<ActionResult<LikeDTOwObjects>> GetLike(Guid postID, Guid userID)
        {
            var like = await _context.Likes.Include(l => l.Post).Include(l => l.User).Where(l => l.PostId == postID && l.UserId == userID).FirstOrDefaultAsync();

            if (like == null)
            {
                return NotFound();
            }

            return like.ToLikeDTOwObjects();
        }

        // GET: api/Likes/5
        [HttpGet("{postID}")]
        public async Task<ActionResult<IEnumerable<LikeDTOUsers>>> GetPostLikes(Guid postID)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Likes'  is null.");
            }

            if (!PostExists(postID))
            {
                return NotFound("Post with id " + postID + " does not exist.");
            }

            return await _context.Likes.Include(l => l.User).Where(l => l.PostId == postID).Select(l => l.ToLikeDTOUsers()).ToListAsync();
        }

        // GET: api/Likes/counter/5
        [HttpGet("counter/{postID}")]
        public async Task<ActionResult<int>> GetPostLikesCounter(Guid postID)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Likes'  is null.");
            }

            if (!PostExists(postID))
            {
                return NotFound("Post with id " + postID + " does not exist.");
            }

            var likes = await _context.Likes.Where(l => l.PostId == postID).Select(l => l.ToLikeDTO()).CountAsync();
           
            return likes;
        }

        // POST api/Likes
        [HttpPost]
        public async Task<ActionResult<LikeDTO>> PostLike(LikePOSTDTO like)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Likes'  is null.");
            }

            if (!PostExists(like.PostId))
            {
                return NotFound("Post with id " + like.PostId + " does not exist.");
            }

            if (!ProfileExists(like.UserId))
            {
                return NotFound("Profile with id " + like.UserId + " does not exist.");
            }

            Like newlike = like.ToLike();
            _context.Likes.Add(newlike);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LikeExists(newlike))
                {
                    return Conflict("Post already liked.");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLike", new { postID = newlike.PostId, userID = newlike  }, newlike.ToLikeDTO());
        }

        // DELETE: api/Likes/5
        [HttpDelete("{postID}/{userID}")]
        public async Task<IActionResult> DeleteLike(Guid postID, Guid userID)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Likes'  is null.");
            }

            if (!PostExists(postID))
            {
                return NotFound("Post with id " + postID + " does not exist.");
            }

            if (!ProfileExists(userID))
            {
                return NotFound("Profile with id " + userID + " does not exist.");
            }

            var like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postID && l.UserId == userID);

            if (like == null)
            {
                return NotFound("Like with postID " + postID + " and userID " + userID + " does not exist.");
            }

            if (like.UserId != userID)
            {
                return Unauthorized();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }

         bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
         bool ProfileExists(Guid id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
         bool LikeExists(Like newlike)
        {
            return _context.Likes.Any(e => e.PostId == newlike.PostId && e.UserId == newlike.UserId);
        }
    }
}
