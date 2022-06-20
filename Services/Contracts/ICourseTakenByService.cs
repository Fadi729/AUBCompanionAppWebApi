using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ICourseTakenByService
    {
        public Task<IEnumerable<CourseTakenBy_Course_DTO>> GetUsersTakingCourse              (int crn);
        public Task<IEnumerable<CourseTakenBy_User_DTO>>   GetCoursesTakenByUser             (Guid userID);
        public Task<IEnumerable<CourseTakenBy_User_DTO>>   GetCoursesTakenByUserInSemester   (Guid userID, string semesterID);
        public Task<CourseTakenBy_POST_DTO>                AddCourseToUser                   (CourseTakenBy_POST_DTO courseTakenBy);
        public Task                                        DeleteCoursesTakenByUser          (Guid userID, int crn, string semesterID);
    }
}
