using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class CourseTakenByExtensions
    {
        public static CourseTakenByDTO ToCourseTakenByDTO(this CourseTakenBy courseTakenBy)
        {
            return new CourseTakenByDTO
            {
                User     = courseTakenBy.User.ToProfileDTO(),
                Course   = courseTakenBy.CCrnNavigation.ToCourseDTO(),
                Semester = courseTakenBy.Semester.ToSemesterDTO(),
                Grade    = courseTakenBy.Grade,

            };
        }
        public static CourseTakenBy_User_DTO ToCourseTakenBy_User_DTO(this CourseTakenBy courseTakenBy)
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
                User     = courseTakenBy.User.ToProfileDTO(),
                Semester = courseTakenBy.Semester.ToSemesterDTO()
            };
        } 
        public static CourseTakenBy ToCourseTakenBy(this CourseTakenBy_POST_DTO courseTakenBy)
        {
            return new CourseTakenBy
            {
                UserId     = courseTakenBy.UserId,
                CCrn       = courseTakenBy.CCrn,
                SemesterId = courseTakenBy.SemesterId,
                Grade      = courseTakenBy.Grade
            };
        }
    }
}
