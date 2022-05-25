using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly CompanionAppDBContext _context;

        public PostsController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            return await _context.Posts.Include(p => p.User).Select(p => p.ToPostDTO()).ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById(Guid id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return post.ToPostDTO();
        }

        // GET: api/Posts/5
        //[Route("api/[controller]/getpostbyuserId/")]
        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<PostsByUserDTO>>> GetPostsByUserID(Guid userID)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            List<PostsByUserDTO> posts = await _context.Posts.FromSqlRaw("SELECT * FROM CompanionApp.POST WHERE userID = {0}", userID).Select(p => p.ToPostsByUserDTO()).ToListAsync();

            if (posts == null)
            {
                return NotFound();
            }

            return posts;
        }

        [HttpGet("user/followings/{userID}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsByUserFollowings(Guid userID)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            List<PostDTO> posts = await _context.Posts.FromSqlRaw("SELECT * FROM CompanionApp.POST WHERE userID IN (SELECT Is_Following FROM CompanionApp.FOLLOWING WHERE userID = {0})", userID).Include(p => p.User).Select(p => p.ToPostDTO()).ToListAsync();

            if (posts == null)
            {
                return NotFound();
            }

            return posts;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userID}/{id}")]
        public async Task<IActionResult> PutPost(Guid id, Guid userID, PostPUTDTO post)
        {
            
            var postToUpdate = await _context.Posts.FindAsync(id);
            
            if (postToUpdate == null)
            {
                return NotFound();
            }
            
            if (userID != postToUpdate.UserId)
            {
                return Unauthorized();
            }
            
            postToUpdate.Text       = post.Text;
            postToUpdate.Attachment = post.Attachment;
            
            _context.Entry(postToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(PostPOSTDTO post)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Posts'  is null.");
            }

            Post newpost = post.ToPost();
            _context.Posts.Add(newpost);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostExists(newpost.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPostById", new { id = newpost.Id }, newpost.ToPostDTO());
        }

        // DELETE: api/Posts/5
        [HttpDelete("{userID}/{id}")]
        public async Task<IActionResult> DeletePost(Guid id, Guid userID)
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

        private bool PostExists(Guid id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
