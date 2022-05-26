using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CompanionAppDBContext _context;

        public CoursesController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            return await _context.Courses.Select(c => c.ToCourseDTO()).ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course.ToCourseDTO();
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDTO course)
        {
            if (id != course.Crn)
            {
                return BadRequest("id and crn must match");
            }

            _context.Entry(course.ToCourse()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> PostCourse(CourseDTO course)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'MyDatabaseContext.Courses'  is null.");
            }
            _context.Courses.Add(course.ToCourse());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(course.Crn))
                {
                    return Conflict("Course Already Exists");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCourse", new { id = course.Crn }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Crn == id)).GetValueOrDefault();
        }
    }
}
