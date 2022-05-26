using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseTakenByController : ControllerBase
    {
        private readonly CompanionAppDBContext _context;

        public CourseTakenByController(CompanionAppDBContext context)
        {
            _context = context;
        }

        // GET: api/CourseTakenBy
        [HttpGet("{userID}/{crn}/{semesterID}")]
        public async Task<ActionResult<CourseTakenByDTO>> GetCoursesTakenBy(Guid userID, int crn, string semesterID)
        {
            if (_context.CourseTakenBy == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.CourseTakenBy'  is null.");
            }

            if (!ProfileExists(userID))
            {
                return NotFound("Profile Not Found");
            }

            if (!CourseExists(crn))
            {
                return NotFound("Course Not Found");
            }

            if (!SemesterExists(semesterID))
            {
                return NotFound("Semester Not Found");
            }

            return await _context.CourseTakenBy.Include(c => c.CCrnNavigation).Include(y => y.Semester).Include(w => w.User)
                .Where(x => x.UserId == userID && x.CCrn == crn && x.SemesterId == semesterID).Select(x => x.ToCourseTakenByDTO()).FirstOrDefaultAsync();
        }

        // GET: api/CourseTakenBy/user/5
        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_User_DTO>>> GetCoursesTakenByUser(Guid userID)
        {
            if (_context.CourseTakenBy == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.CourseTakenBy'  is null.");
            }

            if (!ProfileExists(userID))
            {
                return NotFound("Profile with id " + userID + " does not exist.");
            }

            return await _context.CourseTakenBy.Include(c => c.CCrnNavigation).Include(y => y.Semester).Where(x => x.UserId == userID).Select(x => x.ToCourseTakenBy_User_DTO()).ToListAsync();
        }

        // GET: api/CourseTakenBy/course/
        [HttpGet("course/{crn}")]
        public async Task<ActionResult<IEnumerable<CourseTakenBy_Course_DTO>>> GetUserTakingCourse(int crn)
        {
            if (_context.CourseTakenBy == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.CourseTakenBy'  is null.");
            }

            if (!CourseExists(crn))
            {
                return NotFound("Course with id " + crn + " does not exist.");
            }

            return await _context.CourseTakenBy.Include(c => c.User).Include(y => y.Semester).Where(x => x.CCrn == crn).Select(x => x.ToCourseTakenBy_Course_DTO()).ToListAsync();
        }

        //POST: api/CoursesTakenBy/user
        [HttpPost]
        public async Task<ActionResult<CourseTakenBy_User_DTO>> PostUserTakingCourse(CourseTakenBy_POST_DTO courseTakenBy)
        {
            if (_context.CourseTakenBy == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.CourseTakenBy'  is null.");
            }

            if (!ProfileExists(courseTakenBy.UserId))
            {
                return NotFound("Profile Not Found");
            }

            if (!CourseExists(courseTakenBy.CCrn))
            {
                return NotFound("Course Not Found");
            }

            if (!SemesterExists(courseTakenBy.SemesterId))
            {
                return NotFound("Semester Not Found");
            }

            CourseTakenBy newCourseTakenBy = courseTakenBy.ToCourseTakenBy();

            _context.CourseTakenBy.Add(newCourseTakenBy);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfileTookCourse(newCourseTakenBy.UserId, newCourseTakenBy.CCrn, newCourseTakenBy.SemesterId))
                {
                    return Conflict("Profile Already Took The Course");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCoursesTakenBy", new { userID = courseTakenBy.UserId, crn = courseTakenBy.CCrn, semesterID = courseTakenBy.SemesterId }, courseTakenBy);
        }

        // DELETE: api/CoursesTakenBy/5/6/7
        [HttpDelete("{userID}/{crn}/{semesterID}")]
        public async Task<ActionResult<CourseTakenByDTO>> DeleteCoursesTakenBy(Guid userID, int crn, string semesterID)
        {
            if (_context.CourseTakenBy == null)
            {
                return Problem("Entity set 'CompanionAppDBContext.CourseTakenBy'  is null.");
            }

            if (!ProfileExists(userID))
            {
                return NotFound("Profile Not Found");
            }

            if (!CourseExists(crn))
            {
                return NotFound("Course Not Found");
            }

            if (!SemesterExists(semesterID))
            {
                return NotFound("Semester Not Found");
            }

            var courseTakenBy = await _context.CourseTakenBy.FindAsync(userID, crn, semesterID);

            if (courseTakenBy == null)
            {
                return NotFound("Profile Did Not Take The Course");
            }

            if (courseTakenBy.UserId != userID)
            {
                return Unauthorized();
            }

            _context.CourseTakenBy.Remove(courseTakenBy);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProfileExists(Guid id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
        private bool CourseExists(int crn)
        {
            return _context.Courses.Any(e => e.Crn == crn);
        }
        private bool SemesterExists(string id)
        {
            return _context.Semesters.Any(e => e.Id == id);
        }
        private bool ProfileTookCourse(Guid userID, int crn, string semesterID)
        {
            return _context.CourseTakenBy.Any(e => e.UserId == userID && e.CCrn == crn && e.SemesterId == semesterID);
        }

    }
}
