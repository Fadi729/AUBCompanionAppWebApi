using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        public Task<CourseDTO>              GetCourseAsync    (int crn);
        public Task<CourseDTO>              AddCourseAsync    (CourseDTO course);
        public Task<IEnumerable<CourseDTO>> AddCoursesAsync   (IEnumerable<CourseDTO> courses);
        public Task                         EditCourseAsync   (CourseDTO course);
        public Task                         DeleteCourseAsync (int crn);
    }
}
