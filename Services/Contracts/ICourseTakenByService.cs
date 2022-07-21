using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ICourseTakenByService
    {
        public Task<IEnumerable<CourseTakenBy_Course_DTO>> GetUsersTakingCourse           (int crn,                                 CancellationToken cancellationToken);
        public Task<IEnumerable<CourseTakenBy_User_DTO>>   GetCoursesTakenByUser          (Guid userID,                             CancellationToken cancellationToken);
        public Task<IEnumerable<CourseTakenBy_User_DTO>>   GetCoursesTakenByUserInSemester(Guid userID, string semesterID,          CancellationToken cancellationToken);
        public Task<CourseTakenBy_POST_DTO>                AddCourseToUser                (CourseTakenBy_POST_DTO courseTakenBy,    CancellationToken cancellationToken);
        public Task                                        DeleteCoursesTakenByUser       (Guid userID, int crn, string semesterID, CancellationToken cancellationToken);
    }
}
