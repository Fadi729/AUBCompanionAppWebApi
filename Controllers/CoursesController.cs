using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

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
            return Ok(await _courseService.GetAllCoursesAsync());
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>>              GetCourse   (int id)
        {
            return Ok(await _courseService.GetCourseAsync(id));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult>                        PutCourse   (int id, CourseDTO course)
        {
            await _courseService.EditCourseAsync(id, course);
            return NoContent();
        }
        
        [HttpPost("single")]
        public async Task<ActionResult<CourseDTO>>              PostCourse  (CourseDTO course)
        {
            CourseDTO newCourse = await _courseService.AddCourseAsync(course);
            return CreatedAtAction("GetCourse", new { id = newCourse.Crn }, newCourse);
            
        }

        /// <summary>
        /// Adds A List of Courses
        /// </summary>
        /// <param name="courses">Courses to Add</param>
        /// <response code="200">A List of courses that were not added</response>
        [HttpPost("many")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> PostCourses (IEnumerable<CourseDTO> courses)
        {
            return Ok((await _courseService.AddCoursesAsync(courses)).ToList());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult>                        DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
