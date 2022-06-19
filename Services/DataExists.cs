using CompanionApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Services
{
    public static class DataExists
    {
        public static async Task<bool> ProfileExists (this DbSet<Profile> _dbSet, Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }
        public static async Task<bool> ProfileExists (this DbSet<Profile> _dbSet, string? email)
        {
            return await _dbSet.AnyAsync(e => e.Email == email);
        }
        public static async Task<bool> CourseExists  (this DbSet<Course>  _dbSet, int crn)
        {
            return await _dbSet.AnyAsync(e => e.Crn == crn);
        }
        public static async Task<bool> PostExists    (this DbSet<Post>    _dbSet, Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }
        public static async Task<bool> SemesterExists(this DbSet<Semester> _dbset, string id)
        {
            return await _dbset.AnyAsync(e => e.Id == id);
        }
        public static async Task<bool> CommentExists (this DbSet<Comment> _dbset, Guid id)
        {
            return await _dbset.AnyAsync(c => c.Id == id);
        }
    }
}
