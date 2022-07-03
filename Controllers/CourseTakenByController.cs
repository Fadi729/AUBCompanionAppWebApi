using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseTakenByController : ControllerBase
    {
        readonly ICourseTakenByService _courseTakenByService;

        public CourseTakenByController(ICourseTakenByService CourseTakenByService)
        {
            _courseTakenByService = CourseTakenByService;
        }


        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_User_DTO>>>   GetCoursesTakenByUser          (Guid userID)
        {
            return Ok(await _courseTakenByService.GetCoursesTakenByUser(userID));
        }
        
        
        [HttpGet("user/{userID}/semester/{semesterID}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_User_DTO>>>   GetCoursesTakenByUserInSemester(Guid userID, string semesterID)
        {
            return Ok(await _courseTakenByService.GetCoursesTakenByUserInSemester(userID, semesterID));
        }

        
        [HttpGet("course/{crn}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_Course_DTO>>> GetUsersTakingCourse           (int crn)
        {
            return Ok(await _courseTakenByService.GetUsersTakingCourse(crn));
        }

        
        [HttpPost]
        public async Task<ActionResult<CourseTakenBy_POST_DTO>>                PostUserTakingCourse           (CourseTakenBy_POST_DTO courseTakenBy)
        {
            return await _courseTakenByService.AddCourseToUser(courseTakenBy);
        }

        
        [HttpDelete("{crn}/{semesterID}")]
        public async Task<IActionResult>                                       DeleteCoursesTakenBy           (int crn, string semesterID)
        {
            await _courseTakenByService.DeleteCoursesTakenByUser(HttpContext.GetUserID(), crn, semesterID);
            return NoContent();
        }
    }
}
