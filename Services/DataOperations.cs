﻿using CompanionApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Services
{
    public static class DataOperations
    {
        public static async Task<bool>     ProfileExists        (this DbSet<Profile>       _dbSet, Guid userID)
        {
            return await _dbSet.AnyAsync(profile => profile.Id == userID);
        }
        public static async Task<bool>     ProfileExists        (this DbSet<Profile>       _dbSet, string? email)
        {
            return await _dbSet.AnyAsync(profile => profile.Email == email);
        }
        public static async Task<bool>     CourseExists         (this DbSet<Course>        _dbSet, string crn)
        {
            return await _dbSet.AnyAsync(course => course.Crn.ToString().Equals(crn));
        }
        public static async Task<bool>     SemesterExists       (this DbSet<Semester>      _dbset, string semesterID)
        {
            return await _dbset.AnyAsync(semester => semester.Id == semesterID);
        }
        public static async Task<bool>     CourseGivenInSemester(this DbSet<Course>        _dbSet, string crn, string semesterID)
        {
            return await _dbSet.AnyAsync(course => course.Crn.ToString().Equals(crn) && course.SemesterId == semesterID);
        }
        public static async Task<bool>     ProfileTookCourse    (this DbSet<CourseTakenBy> _dbset, Guid userID, int crn, string semesterID)
        {
            return await _dbset.AnyAsync(coursetakenby => coursetakenby.UserId == userID && coursetakenby.CCrn == crn && coursetakenby.SemesterId == semesterID);
        }
        public static async Task<bool>     PostExists           (this DbSet<Post>          _dbSet, string postID)
        {
            return await _dbSet.AnyAsync(post => post.Id.ToString().Equals(postID));
        }
        public static async Task<Post?>    GetPost              (this DbSet<Post>          _dbSet, string postID)
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(post => post.Id.ToString().Equals(postID));
        }
        public static       bool           UserOwnsPost         (string postUserID, string userID)
        {
            return postUserID == userID;
        }
        public static async Task<bool>     CommentExists        (this DbSet<Comment>       _dbset, string commentID)
        {
            return await _dbset.AnyAsync(comment => comment.Id.ToString().Equals(commentID));
        }
        public static async Task<Comment?> GetComment           (this DbSet<Comment>       _dbset, Guid commentID)
        {
            return await _dbset.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(comment => comment.Id.Equals(commentID));
        }   
        public static       bool           CommentBelongsToPost (Guid commentPostID, Guid postID)
        {
            return commentPostID == postID;
        }
        public static       bool           UserOwnsComment      (Guid commentUserID, Guid userID)
        {
            return commentUserID == userID;
        }
        public static async Task<bool>     LikeExists           (this DbSet<Like>          _dbset, string postID, string userID)
        {
            return await _dbset.AnyAsync(like => like.PostId.ToString().Equals(postID) && like.UserId.ToString().Equals(userID));
        }
        public static async Task<bool>     FollowingExists      (this DbSet<Following>     _dbset, string userID, string followingID)
        {
            return await _dbset.AnyAsync(following => following.UserId.ToString().Equals(userID) && following.IsFollowing.ToString().Equals(followingID));
        }
    }
}