using FluentValidation;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.CourseExceptions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        readonly ICourseService _courseService;

        public CoursesController(ICourseService repository)
        {
            _courseService = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses  ()
        {
            try
            {
                IEnumerable<CourseDTO> courses = await _courseService.GetAllCoursesAsync();
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
        public async Task<ActionResult<CourseDTO>>              GetCourse   (int id)
        {
            try
            {
                return Ok(await _courseService.GetCourseAsync(id));
            }
            catch (CourseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult>                        PutCourse   (int id, CourseDTO course)
        {
            try
            {
                await _courseService.EditCourseAsync(id, course);
                return NoContent();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ValidationException)
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
        public async Task<ActionResult<CourseDTO>>              PostCourse  (CourseDTO course)
        {
            try
            {
                CourseDTO newCourse = await _courseService.AddCourseAsync(course);
                return CreatedAtAction("GetCourse", new { id = newCourse.Crn }, newCourse);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch(CourseAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
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
        public async Task<ActionResult<IEnumerable<CourseDTO>>> PostCourses (IEnumerable<CourseDTO> courses)
        {
            try
            {
                return Ok((await _courseService.AddCoursesAsync(courses)).ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult>                        DeleteCourse(int id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);
                return NoContent();
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
        }
    }
