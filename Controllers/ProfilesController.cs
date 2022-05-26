using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly CompanionAppDBContext _context;

        public ProfilesController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetProfiles()
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }

            return await _context.Profiles.Select(p => p.ToProfileDTO()).ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(Guid id)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Profiles'  is null.");
            }
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile.ToProfileDTO();
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(Guid id, ProfileDTOPUT profile)
        {
            //if (id != profile.Id)
            //{
            //    return BadRequest();
            //}

            if (!ProfileExists(id))
            {
                return NotFound();
            }

            _context.Entry(profile.ToProfile(id)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException)
            {
                if (ProfileExists(profile.Email))
                {
                    return Conflict("Email already exists.");
                }
                else
                {
                    throw;
                }
            }


            return NoContent();
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfileDTO>> PostProfile(ProfileDTOPOST profile)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Profiles'  is null.");
            }
            Profile newprofile = profile.ToProfile();
            newprofile.Id = Guid.NewGuid();
            _context.Profiles.Add(newprofile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfileExists(profile.Email))
                {
                    return Conflict("Email already exists.");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfile", new { id = newprofile.ToProfileDTO().Id }, newprofile.ToProfileDTO());
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            if (id != profile.Id)
            {
                return Unauthorized();
            }


            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(Guid id)
        {
            return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool ProfileExists(string? email)
        {
            return (_context.Profiles?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}
