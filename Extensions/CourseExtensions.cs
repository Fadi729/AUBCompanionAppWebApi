using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class CourseExtensions
    {
        public static CourseDTO ToCourseDTO(this Course course)
        {
            return new CourseDTO
            {
                Crn           = course.Crn,
                Title         = course.Title,
                Subject       = course.Subject,
                Code          = course.Code,
                Credits       = course.Credits,
                Attribute     = course.Attribute,
                Days          = course.Days,
                StartTime     = course.StartTime.HasValue ? course.StartTime.Value.ToString()[..5] : string.Empty,
                EndTime       = course.EndTime.HasValue   ? course.EndTime  .Value.ToString()[..5] : string.Empty,
                Location      = course.Location,
                Instructor    = course.Instructor,
                Restrictions  = course.Restrictions,
                Prerequisites = course.Prerequisites,
                SemesterId    = course.SemesterId,
            };
        }

        public static Course ToCourse(this CourseDTO courseDTO)
        {
            return new Course
            {
                Crn           = courseDTO.Crn,
                Title         = courseDTO.Title,
                Subject       = courseDTO.Subject,
                Code          = courseDTO.Code,
                Credits       = courseDTO.Credits,
                Attribute     = courseDTO.Attribute,
                Days          = courseDTO.Days,
                StartTime     = !string.IsNullOrEmpty(courseDTO.StartTime) ? TimeSpan.Parse(courseDTO.StartTime) : null,
                EndTime       = !string.IsNullOrEmpty(courseDTO.EndTime)   ? TimeSpan.Parse(courseDTO.EndTime)   : null,
                Location      = courseDTO.Location,
                Instructor    = courseDTO.Instructor,
                Restrictions  = courseDTO.Restrictions,
                Prerequisites = courseDTO.Prerequisites,
                SemesterId    = courseDTO.SemesterId,
            };
        }     
    }
}
