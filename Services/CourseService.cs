using FluentValidation;
using CompanionApp.Models;
using CompanionApp.Validation;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.CourseExceptions;

namespace CompanionApp.Services
{
    public class CourseService : ICourseService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Course>         _dbSet;
        readonly CourseValidation      _courseValidation;
        public CourseService(CompanionAppDBContext DBcontext, CourseValidation validation)
        {
            _context          = DBcontext;
            _dbSet            = DBcontext.Courses;
            _courseValidation = validation;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            IEnumerable<CourseDTO> courseDTOs = await _dbSet.Select(c => c.ToCourseDTO()).ToListAsync();
            if (courseDTOs is null)
            {
                throw new NoCoursesFoundException();
            }
            return courseDTOs;
        }
        public async Task<CourseDTO>              GetCourseAsync    (int crn)
        {
            Course? course = await _dbSet.FindAsync(crn);
            if (course is null)
            {
                throw new CourseNotFoundException();
            }
            return course.ToCourseDTO();
        }
        public async Task<CourseDTO>              AddCourseAsync    (CourseDTO course)
        {
            #region try block
            try
            {
                await _courseValidation.ValidateAndThrowAsync(course);
                if (CourseExists(course.Crn))
                {
                    throw new CourseAlreadyExistsException();
                }
                Course newCourse = course.ToCourse();
                _dbSet.Add(newCourse);
                await _context.SaveChangesAsync();
                return newCourse.ToCourseDTO();
            }
            #endregion
            #region catch block
            catch (ValidationException ex)
            {
                throw new CourseCommandException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
            #endregion
        }
        public async Task<IList<CourseDTO>>       AddCoursesAsync   (IEnumerable<CourseDTO> courses)
        {
            IList<CourseDTO> failedToAddCourses = new List<CourseDTO>();
            #region try   block
            try
            {
                foreach (CourseDTO course in courses)
                {
                    if (!(await _courseValidation.ValidateAsync(course)).IsValid)
                    {
                        failedToAddCourses.Add(course);
                    }
                    else if (CourseExists(course.Crn))
                    {
                        _dbSet.Update(course.ToCourse());
                    }
                    else
                        _dbSet.Add(course.ToCourse());
                }

                await _context.SaveChangesAsync();

                return failedToAddCourses;
            }
            #endregion
            #region catch block
            catch (Exception ex)
            {
                throw new CourseCommandException(ex.Message);
            } 
            #endregion
        }
        public async Task                         EditCourseAsync   (int crn, CourseDTO course)
        {
            #region try   block
            try
            {
                if (crn != course.Crn)
                {
                    throw new ArgumentException("crn and course.crn must match");
                }

                if (!CourseExists(crn))
                {
                    throw new CourseNotFoundException();
                }

                await _courseValidation.ValidateAndThrowAsync(course);
                _dbSet.Update(course.ToCourse());
                await _context.SaveChangesAsync();
            }
            #endregion
            #region catch block
            catch (ValidationException ex)
            {
                throw new CourseCommandException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
            #endregion
        }
        public async Task                         DeleteCourseAsync (int crn)
        {
            try
            {
                if (!CourseExists(crn))
                {
                    throw new CourseNotFoundException();
                }
                Course course = new() { Crn = crn };
                _dbSet.Remove(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        bool CourseExists(int crn)
        {
            return _dbSet.Any(e => e.Crn == crn);
        }
    }
}
