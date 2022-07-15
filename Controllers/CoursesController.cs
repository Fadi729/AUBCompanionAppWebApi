using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoursesController : ControllerBase
    {
        readonly ICourseService _courseService;

        public CoursesController(ICourseService repository)
        {
            _courseService = repository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses  (CancellationToken cancellationToken)
        {
            return Ok(await _courseService.GetAllCoursesAsync(cancellationToken));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>>              GetCourse   (int id, CancellationToken cancellationToken)
        {
            return Ok(await _courseService.GetCourseAsync(id, cancellationToken));
        }


        [HttpPut]
        public async Task<IActionResult>                        PutCourse   (CourseDTO course, CancellationToken cancellationToken)
        {
            await _courseService.EditCourseAsync(course, cancellationToken);
            return NoContent();
        }

        
        [HttpPost("single")]
        public async Task<ActionResult<CourseDTO>>              PostCourse  (CourseDTO course, CancellationToken cancellationToken)
        {
            
            CourseDTO newCourse = await _courseService.AddCourseAsync(course, cancellationToken);
            return CreatedAtAction("GetCourse", new { id = newCourse.Crn }, newCourse);
            
        }

        /// <summary>
        /// Adds A List of Courses
        /// </summary>
        /// <param name="courses">Courses to Add</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">A List of courses that were not added</response>
        [HttpPost("many")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> PostCourses (IEnumerable<CourseDTO> courses, CancellationToken cancellationToken)
        {
            return Ok((await _courseService.AddCoursesAsync(courses, cancellationToken)).ToList());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult>                        DeleteCourse(int id, CancellationToken cancellationToken)
        {
            await _courseService.DeleteCourseAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
