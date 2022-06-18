using CompanionApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Services
{
    public static class DataExists
    {
        public static bool ProfileExists(this DbSet<Profile> _dbSet, Guid id)
        {
            return _dbSet.Any(e => e.Id == id);
        }
        public static bool ProfileExists(this DbSet<Profile> _dbSet, string? email)
        {
            return _dbSet.Any(e => e.Email == email);
        }
        public static bool CourseExists (this DbSet<Course>  _dbSet, int crn)
        {
            return _dbSet.Any(e => e.Crn == crn);
        }
        public static bool PostExists   (this DbSet<Post>    _dbSet, Guid id)
        {
            return _dbSet.Any(e => e.Id == id);
        }
    }
}
