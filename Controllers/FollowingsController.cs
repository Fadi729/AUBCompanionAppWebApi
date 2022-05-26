using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingsController : ControllerBase
    {
        private readonly CompanionAppDBContext _context;

        public FollowingsController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Followings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FollowingDTO>>> GetFollowing(Guid id)
        {
          if (_context.Followings == null)
          {
              return NotFound();
          }
            var following = await _context.Followings.Include(f => f.IsFollowingNavigation).Where(f => f.UserId == id).ToListAsync();

            if (following == null)
            {
                return NotFound();
            }

            return following.Select(f => f.ToFollowingDTO()).ToList();
        }

        // PUT: api/Followings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFollowing(Guid id, Following following)
        //{
        //    if (id != following.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(following).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FollowingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        
        // POST: api/Followings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Following>> PostFollowing(FollowingPOSTDTO following)
        {
          if (_context.Followings == null)
          {
              return Problem("Entity set 'CompanionAppDBContext.Followings'  is null.");
          }
            Following newfollowing = following.ToFollowing();
            _context.Followings.Add(newfollowing);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowingExists(following.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFollowing", new { id = following.UserId }, following);
        }

        // DELETE: api/Followings/
        [HttpDelete]
        public async Task<IActionResult> DeleteFollowing(FollowingPOSTDTO following)
        {
            if (_context.Followings == null)
            {
                return NotFound();
            }
            var deletefollowing = await _context.Followings.FindAsync(following.UserId, following.IsFollowing);
            if (deletefollowing == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(deletefollowing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowingExists(Guid id)
        {
            return (_context.Followings?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
