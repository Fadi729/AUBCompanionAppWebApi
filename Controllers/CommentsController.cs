using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class CommentsController : ControllerBase
    {
         readonly CompanionAppDBContext _context;

        public CommentsController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(Guid id)
        {
            var comment = await _context.Comments.Include(c => c.User).Where(c => c.Id == id).FirstOrDefaultAsync();

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return comment.ToCommentDTO();
        }

        // GET: api/Comments/post/5
        [HttpGet("post/{postID}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetPostComments(Guid postID)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Comments'  is null.");
            }

            if (!PostExists(postID))
            {
                return NotFound("Post with id " + postID + " does not exist.");
            }

            return await _context.Comments.Include(l => l.User).Where(l => l.PostId == postID).Select(l => l.ToCommentDTO()).ToListAsync();
        }

        // POST api/Comments
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentPOSTDTO comment)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Likes'  is null.");
            }

            if (!PostExists(comment.PostId))
            {
                return NotFound("Post with id " + comment.PostId + " does not exist.");
            }

            if (!ProfileExists(comment.UserId))
            {
                return NotFound("Profile with id " + comment.UserId + " does not exist.");
            }

            Comment newComment = comment.ToComment();
            
            _context.Comments.Add(newComment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetComment", new { id = newComment.Id }, comment);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}/{userID}")]
        public async Task<IActionResult> PutComment(Guid id, Guid userID,CommentPUTDTO comment)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Comments'  is null.");
            }

            if (!CommentExists(id))
            {
                return NotFound("Comment with id " + id + " does not exist.");
            }
            
            var commentToUpdate = await _context.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
            
            if (commentToUpdate == null)
            {
                return NotFound("Comment with id " + id + " does not exist.");
            }

            if (!ProfileExists(userID))
            {
                return NotFound("Profile with id " + userID + " does not exist.");
            }

            if (commentToUpdate.UserId != userID)
            {
                return Unauthorized();
            }

            commentToUpdate.Text = comment.Text;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound("Comment with id " + id + " does not exist.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}/{userID}")]
        public async Task<IActionResult> DeleteComment(Guid id, Guid userID)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Likes'  is null.");
            }

            if (!CommentExists(id))
            {
                return NotFound("Comment with id " + id + " does not exist.");
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment with id " + id + " does not exist.");
            }

            if (comment.UserId != userID)
            {
                return Unauthorized();
            }

            _context.Comments.Remove(comment);
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
         bool CommentExists(Guid id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
