using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;

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

        // GET: api/Followings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Following>>> GetFollowings()
        {
          if (_context.Followings == null)
          {
              return NotFound();
          }
            return await _context.Followings.ToListAsync();
        }

        // GET: api/Followings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Following>> GetFollowing(Guid id)
        {
          if (_context.Followings == null)
          {
              return NotFound();
          }
            var following = await _context.Followings.FindAsync(id);

            if (following == null)
            {
                return NotFound();
            }

            return following;
        }

        // PUT: api/Followings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowing(Guid id, Following following)
        {
            if (id != following.UserId)
            {
                return BadRequest();
            }

            _context.Entry(following).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingExists(id))
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

        // POST: api/Followings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Following>> PostFollowing(Following following)
        {
          if (_context.Followings == null)
          {
              return Problem("Entity set 'CompanionAppDBContext.Followings'  is null.");
          }
            _context.Followings.Add(following);
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

        // DELETE: api/Followings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowing(Guid id)
        {
            if (_context.Followings == null)
            {
                return NotFound();
            }
            var following = await _context.Followings.FindAsync(id);
            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowingExists(Guid id)
        {
            return (_context.Followings?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
