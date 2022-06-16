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

        /// <summary>
        /// Adds A List of Courses
        /// </summary>
        /// <param name="courses">Courses to Add</param>
        /// <response code="200">A List of courses that were not added</response>
        [HttpPost("many")]
        public async Task<ActionResult<IList<CourseDTO>>> PostCourses(List<CourseDTO> courses)
        {
            try
            {
                return (await CourseRepo.AddCoursesAsync(courses)).ToList();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await CourseRepo.DeleteCourseAsync(id);
                return NoContent();
            }
            catch (CourseNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }

    }
}
