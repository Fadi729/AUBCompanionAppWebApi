using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseDTO>> GetAllCoursesAsync(                                CancellationToken cancellationToken);
        public Task<CourseDTO>              GetCourseAsync    (int crn,                        CancellationToken cancellationToken);
        public Task<CourseDTO>              AddCourseAsync    (CourseDTO course,               CancellationToken cancellationToken);
        public Task<IEnumerable<CourseDTO>> AddCoursesAsync   (IEnumerable<CourseDTO> courses, CancellationToken cancellationToken);
        public Task                         EditCourseAsync   (CourseDTO course,               CancellationToken cancellationToken);
        public Task                         DeleteCourseAsync (int crn,                        CancellationToken cancellationToken);
    }
}
            