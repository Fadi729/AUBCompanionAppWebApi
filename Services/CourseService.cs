using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
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
            IEnumerable<CourseDTO> coursesDTOs = await _dbSet.Select(c => c.ToCourseDTO()).ToListAsync();
            if (!coursesDTOs.Any())
            {
                throw new NoCoursesFoundException();
            }
            return coursesDTOs;
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
                Course newCourse = course.ToCourse();
                _dbSet.Add(newCourse);
                await _context.SaveChangesAsync();
                return newCourse.ToCourseDTO();
            }
            #endregion
            #region catch block
            catch(UniqueConstraintException)
            {
                throw new CourseAlreadyExistsException();
            }
            #endregion
        }
        public async Task<IEnumerable<CourseDTO>> AddCoursesAsync   (IEnumerable<CourseDTO> courses)
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
                    else if (await _dbSet.CourseExists(course.Crn))
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
            catch (Exception)
            {
                throw;
            } 
            #endregion
        }
        public async Task                         EditCourseAsync   (int crn, CourseDTO course)
        {
            
            if (crn != course.Crn)
                {
                    throw new ArgumentException("crn and course.crn must match");
                }
            if (!await _dbSet.CourseExists(crn))
                {
                    throw new CourseNotFoundException();
                }

            await _courseValidation.ValidateAndThrowAsync(course);
            _dbSet.Update(course.ToCourse());
            await _context.SaveChangesAsync();
            
        }
        public async Task                         DeleteCourseAsync (int crn)
        {
            try
            {
                if (!await _dbSet.CourseExists(crn))
                {
                    throw new CourseNotFoundException();
                }
                _dbSet.Remove(new Course() { Crn = crn });
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

