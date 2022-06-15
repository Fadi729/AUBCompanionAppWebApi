using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class SemestersController : ControllerBase
    {
         readonly CompanionAppDBContext _context;

        public SemestersController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Semesters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> GetSemesters()
        {
            return await _context.Semesters.Select(x => x.ToSemesterDTO()).ToListAsync();
        }

        // GET: api/Semesters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterDTO>> GetSemester(Guid id)
        {
            if (_context.Semesters == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Semesters'  is null.");
            }
            var semester = await _context.Semesters.FindAsync(id);

            if (semester == null)
            {
                return NotFound();
            }

            return semester.ToSemesterDTO();
        }

        // POST: api/Semesters
        [HttpPost("single")]
        public async Task<ActionResult<SemesterDTO>> PostSemester(SemesterDTO semesterDTO)
        {
            if (_context.Semesters == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Semesters'  is null.");
            }

            _context.Semesters.Add(semesterDTO.ToSemester());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SemesterExists(semesterDTO.Id))
                {
                    return Conflict("Semester already exists.");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSemester", new { id = semesterDTO.Id }, semesterDTO);
        }
        
        // POST: api/Semesters
        [HttpPost("many")]
        public async Task<ActionResult<SemesterDTO>> PostSemesters(List<SemesterDTO> semesters)
        {
            if (_context.Semesters == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Semesters'  is null.");
            }

           semesters.ForEach(semester =>
            {
                if(SemesterExists(semester.Id))
                {
                    _context.Entry(semester.ToSemester()).State = EntityState.Modified;    
                }else
                     _context.Semesters.Add(semester.ToSemester());
            });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            if (_context.Semesters == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.Semesters'  is null.");
            }

            var semester = await _context.Semesters.FindAsync(id);

            if (semester == null)
            {
                return NotFound();
            }

            _context.Semesters.Remove(semester);
            await _context.SaveChangesAsync();

            return NoContent();
        }

         bool SemesterExists(string id)
        {
            return _context.Semesters.Any(e => e.Id == id);
        }
    }
}


