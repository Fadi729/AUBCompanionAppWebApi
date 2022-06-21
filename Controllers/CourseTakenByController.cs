using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        
        [HttpDelete("{userID}/{crn}/{semesterID}")]
        public async Task<ActionResult<CourseTakenByDTO>>                      DeleteCoursesTakenBy           (Guid userID,int crn, string semesterID)
        {
            await _courseTakenByService.DeleteCoursesTakenByUser(userID, crn, semesterID);
            return NoContent();
        }
    }
}
