using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using System.Diagnostics;

namespace CompanionApp.Extensions
{
    public static class CourseExtensions
    {
        public static CourseDTO ToCourseDTO(this Course course)
        {
            Debug.WriteLine(course.StartTime);
            return new CourseDTO
            {
                Crn        = course.Crn,
                Title      = course.Title,
                Subject    = course.Subject,
                Code       = course.Code,
                Credits    = course.Credits,
                Attribute  = course.Attribute,
                Days       = course.Days,
                StartTime  = course.StartTime.HasValue ? course.StartTime.Value.ToString()[..5] : String.Empty,
                EndTime    = course.EndTime.HasValue ? course.EndTime.Value.ToString()[..5] : String.Empty,
                Location   = course.Location,
                Instructor = course.Instructor
            };
        }

        public static Course ToCourse(this CourseDTO courseDTO)
        {
            return new Course
            {
                Crn          = courseDTO.Crn,
                Title        = courseDTO.Title,
                Subject      = courseDTO.Subject,
                Code         = courseDTO.Code,
                Credits      = courseDTO.Credits,
                Attribute    = courseDTO.Attribute,
                Days         = courseDTO.Days,
                StartTime    = !String.IsNullOrEmpty(courseDTO.StartTime) ? TimeSpan.Parse(courseDTO.StartTime) : null,
                EndTime      = !String.IsNullOrEmpty(courseDTO.EndTime)   ? TimeSpan.Parse(courseDTO.EndTime) : null,
                Location     = courseDTO.Location,
                Instructor   = courseDTO.Instructor
            };
        }     
    }
}
