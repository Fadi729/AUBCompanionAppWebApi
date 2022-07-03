using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class CourseTakenByExtensions
    {
        public static CourseTakenBy_User_DTO   ToCourseTakenBy_User_DTO  (this CourseTakenBy courseTakenBy)
        {
            return new CourseTakenBy_User_DTO
            {
                Grade          = courseTakenBy.Grade,
                Course         = courseTakenBy.CCrnNavigation.ToCourseDTO(),
                Semester       = courseTakenBy.Semester.ToSemesterDTO()
            };
        }
        public static CourseTakenBy_Course_DTO ToCourseTakenBy_Course_DTO(this CourseTakenBy courseTakenBy)
        {
            return new CourseTakenBy_Course_DTO
            {
                Grade    = courseTakenBy.Grade,
                User     = courseTakenBy.User.ToProfileQuerryDTO(),
                Semester = courseTakenBy.Semester.ToSemesterDTO()
            };
        } 
        public static CourseTakenBy            ToCourseTakenBy           (this CourseTakenBy_POST_DTO courseTakenBy)
        {
            return new CourseTakenBy
            {
                UserId     = Guid.Parse(courseTakenBy.UserId.Trim()),
                CCrn       = int.Parse(courseTakenBy.CCrn.Trim()),
                SemesterId = courseTakenBy.SemesterId,
                Grade      = courseTakenBy.Grade
            };
        }
    }
}
