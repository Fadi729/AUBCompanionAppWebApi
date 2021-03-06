using CompanionApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Services
{
    public static class DataOperations
    {
        public static async Task<bool> CourseExists         (this DbSet<Course>   _dbSet, string crn,                    CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(course => course.Crn.ToString().Equals(crn), cancellationToken);
        }
        public static async Task<bool> SemesterExists       (this DbSet<Semester> _dbset, string semesterID,             CancellationToken cancellationToken)
        {
            return await _dbset.AnyAsync(semester => semester.Id == semesterID, cancellationToken);
        }
        public static async Task<bool> CourseGivenInSemester(this DbSet<Course>   _dbSet, string crn, string semesterID, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(course =>
                course.Crn.ToString().Equals(crn) && course.SemesterId == semesterID, cancellationToken);
        }
        public static async Task<bool> PostExists           (this DbSet<Post>     _dbSet, Guid postID,                   CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(post => post.Id.Equals(postID), cancellationToken);
        }
        
        public static async Task<Post?>          GetPostAsync         (this DbSet<Post>          _dbSet, Guid postID, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(post => post.Id.ToString().Equals(postID), cancellationToken);
        }
        public static async Task<Comment?>       GetCommentAsync      (this DbSet<Comment>       _dbset, Guid commentID, CancellationToken cancellationToken)
        {
            return await _dbset.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(comment => comment.Id.Equals(commentID), cancellationToken);
        }
        public static async Task<Like?>          GetLikeAsync         (this DbSet<Like>          _dbset, Guid postID, Guid userID, CancellationToken cancellationToken)
        {
            return await _dbset.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(like => like.PostId.Equals(postID) && like.UserId.Equals(userID), cancellationToken);
        }
        public static async Task<Following?>     GetFollowingAsync    (this DbSet<Following>     _dbset, Guid userID, Guid followingID, CancellationToken cancellationToken)
        {
            return await _dbset.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(following =>
                following.UserId.Equals(userID) && following.IsFollowing.Equals(followingID), cancellationToken);
        }
        public static async Task<CourseTakenBy?> GetCourseTakenByAsync(this DbSet<CourseTakenBy> _dbset, Guid userID, int crn, string semesterID, CancellationToken cancellationToken)
        {
            return await _dbset.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(coursetakenby =>
                coursetakenby.UserId == userID && coursetakenby.CCrn == crn && coursetakenby.SemesterId == semesterID, cancellationToken);
        }

        public static bool UserOwnsPost        (Guid postUserID,          Guid userID)
        {
            return postUserID == userID;
        }
        public static bool UserOwnsComment     (Guid commentUserID,       Guid userID)
        {
            return commentUserID == userID;
        }
        public static bool UserOwnsLike        (Guid likeUserID,          Guid userID)
        {
            return likeUserID == userID;
        }
        public static bool ProfileTookCourse   (Guid courseTakenByUserID, Guid userID)
        {
            return courseTakenByUserID == userID;
        }
        public static bool CommentBelongsToPost(Guid commentPostID,       Guid postID)
        {
            return commentPostID == postID;
        }
    }
}