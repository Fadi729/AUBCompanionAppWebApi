using Microsoft.AspNetCore.Mvc;
using CompanionApp.ModelsDTO;
using CompanionApp.Repositories.Contracts;
using CompanionApp.Exceptions.CourseExceptions;
using CompanionApp.Models;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        ICourseRepository CourseRepo { get; init; }

        public CoursesController(ICourseRepository repository)
        {
            CourseRepo = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            try
            {
                IEnumerable<CourseDTO> courses = await CourseRepo.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (NoCoursesFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            try
            {
                return await CourseRepo.GetCourseAsync(id);    
            }
            catch (CourseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDTO course)
        {
            try
            {
                await CourseRepo.EditCourseAsync(id, course);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CourseCommandException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CourseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("single")]
        public async Task<ActionResult<CourseDTO>> PostCourse(CourseDTO course)
        {
            try
            {
                return await CourseRepo.AddCourseAsync(course);
            }
            catch (CourseAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CourseCommandException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //// POST: api/Courses/5
        //[HttpPost("many")]
        //public async Task<ActionResult<CourseDTO>> PostCourses(List<CourseDTO> courses)
        //{
        //    if (_context.Courses == null)
        //    {
        //        return Problem("Entity set 'MyDatabaseContext.Courses'  is null.");
        //    }

        //    courses.ForEach(course =>
        //    {
        //        if (CourseExists(course.Crn))
        //        {
        //            _context.Entry(course.ToCourse()).State = EntityState.Modified;
        //        }
        //        else
        //            _context.Courses.Add(course.ToCourse());
        //    });

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        throw;
        //    }
        //    return Ok();
        //}

        //// DELETE: api/Courses/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCourse(int id)
        //{
        //    if (_context.Courses == null)
        //    {
        //        return NotFound();
        //    }
        //    var course = await _context.Courses.FindAsync(id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Courses.Remove(course);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

    }
}
