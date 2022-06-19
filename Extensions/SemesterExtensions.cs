using CompanionApp.Models;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Extensions
{
    public static class SemesterExtensions
    {
        public static SemesterDTO ToSemesterDTO(this Semester semester)
        {
            return new SemesterDTO
            {
                Id    = semester.Id,
                Title = semester.Title,
                Year  = semester.Year
            };
        }
        public static Semester    ToSemester   (this SemesterDTO semesterDTO)
        {
            return new Semester
            {
                Id    = semesterDTO.Id,
                Title = semesterDTO.Title,
                Year  = semesterDTO.Year
            };
        }
    }
}
