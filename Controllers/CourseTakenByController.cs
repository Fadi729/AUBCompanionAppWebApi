using FluentValidation;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.CourseExceptions;
using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Exceptions.SemesterExceptions;
using CompanionApp.Exceptions.CourseTakenByExceptions;

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
            try
            {
                return Ok(await _courseTakenByService.GetCoursesTakenByUser(userID));
            }
            catch (NoCoursesTakenByUserException ex)
            {
                return NotFound(ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        [HttpGet("user/{userID}/semester/{semesterID}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_User_DTO>>>   GetCoursesTakenByUserInSemester(Guid userID, string semesterID)
        {
            try
            {
                return Ok(
                    await _courseTakenByService.GetCoursesTakenByUserInSemester(userID, semesterID)
                );
            }
            catch (Exception ex)
                when (ex is NoCoursesTakenByUserException
                    || ex is ProfileNotFoundException
                    || ex is SemesterNotFoundException
                )
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpGet("course/{crn}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_Course_DTO>>> GetUsersTakingCourse           (int crn)
        {
            try
            {
                return Ok(await _courseTakenByService.GetUsersTakingCourse(crn));
            }
            catch (Exception ex)
                when (ex is CourseNotTakenByAnyUserException || ex is CourseNotFoundException)
            {
                return NotFound(ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<CourseTakenBy_POST_DTO>>                PostUserTakingCourse           (CourseTakenBy_POST_DTO courseTakenBy)
        {
            try
            {
                CourseTakenBy_POST_DTO courseTakenBy_Post_DTO =
                    await _courseTakenByService.AddCourseToUser(courseTakenBy);

                return courseTakenBy_Post_DTO;
                
            }
            catch (Exception ex)
                when (ex is ProfileNotFoundException
                    || ex is CourseNotFoundException
                    || ex is SemesterNotFoundException
                    || ex is CourseNotGivenInSemesterException
                )
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch(CourseAlreadyTakenByUserException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpDelete("{userID}/{crn}/{semesterID}")]
        public async Task<ActionResult<CourseTakenByDTO>>                      DeleteCoursesTakenBy           (Guid userID,int crn, string semesterID)
        {
            try
            {
                await _courseTakenByService.DeleteCoursesTakenByUser(userID, crn, semesterID);
                return NoContent();
            }
            catch (Exception ex)
                when (ex is ProfileNotFoundException
                   || ex is CourseNotFoundException
                   || ex is SemesterNotFoundException
                   || ex is CourseNotTakenByUserException
                )
            {
                return NotFound(ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
