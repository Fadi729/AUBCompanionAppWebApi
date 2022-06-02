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
                Crn            = course.Crn,
                Title          = course.Title,
                Subject        = course.Subject,
                Code           = course.Code,
                Credits        = course.Credits,
                Attribute      = course.Attribute,
                SemesterId     = course.SemesterId,
                Section        = course.Section,
                
                Days1          = course.Days1,
                StartTime1     = course.StartTime1.HasValue ? course.StartTime1.Value.ToString()[..5] : string.Empty,
                EndTime1       = course.EndTime1.HasValue   ? course.EndTime1  .Value.ToString()[..5] : string.Empty,
                Location1      = course.Location1,
                Instructor1    = course.Instructor1,
                Type1          = course.Type1,
                
                Days2          = course.Days2,
                StartTime2     = course.StartTime2.HasValue ? course.StartTime2.Value.ToString()[..5] : string.Empty,
                EndTime2       = course.EndTime2.HasValue   ? course.EndTime2  .Value.ToString()[..5] : string.Empty,
                Location2      = course.Location2,
                Instructor2    = course.Instructor2,
                Type2          = course.Type2,

                Restrictions   = course.Restrictions,
                Prerequisites  = course.Prerequisites,
            };
        }
        public static Course ToCourse(this CourseDTO courseDTO)
        {
            return new Course
            {
                Crn            = courseDTO.Crn,
                Title          = courseDTO.Title,
                Subject        = courseDTO.Subject,
                Code           = courseDTO.Code,
                Credits        = courseDTO.Credits,
                Attribute      = courseDTO.Attribute,
                Section        = courseDTO.Section,
                SemesterId     = courseDTO.SemesterId,
                
                Days1          = courseDTO.Days1,
                StartTime1     = !string.IsNullOrEmpty(courseDTO.StartTime1) ? TimeSpan.Parse(courseDTO.StartTime1) : null,
                EndTime1       = !string.IsNullOrEmpty(courseDTO.EndTime1)   ? TimeSpan.Parse(courseDTO.EndTime1)   : null,
                Location1      = courseDTO.Location1,
                Instructor1    = courseDTO.Instructor1,
                Type1          = courseDTO.Type1,
                
                Days2          = courseDTO.Days2,
                StartTime2     = !string.IsNullOrEmpty(courseDTO.StartTime2) ? TimeSpan.Parse(courseDTO.StartTime2) : null,
                EndTime2       = !string.IsNullOrEmpty(courseDTO.EndTime2)   ? TimeSpan.Parse(courseDTO.EndTime2)   : null,
                Location2      = courseDTO.Location2,
                Instructor2    = courseDTO.Instructor2,
                Type2          = courseDTO.Type2,
                
                Restrictions   = courseDTO.Restrictions,
                Prerequisites  = courseDTO.Prerequisites,
            };
        }     
    }
}
