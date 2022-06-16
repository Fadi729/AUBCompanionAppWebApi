using CompanionApp.Exceptions.CourseExceptions;
using CompanionApp.Extensions;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Repositories.Contracts;
using CompanionApp.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        readonly CompanionAppDBContext _context;
        readonly CourseValidation      _courseValidation;
        public CourseRepository(CompanionAppDBContext DBcontext, CourseValidation validation)
        {
            _context = DBcontext;
            _courseValidation = validation;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            IEnumerable<CourseDTO> courseDTOs = await _context.Courses.Select(c => c.ToCourseDTO()).ToListAsync();
            if (courseDTOs is null)
            {
                throw new NoCoursesFoundException();
            }
            return courseDTOs;
        }
        public async Task<CourseDTO>              GetCourseAsync    (int crn)
        {
            Course? course = await _context.Courses.FindAsync(crn);
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
                _context.Entry(newCourse).State = EntityState.Added;
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
                        _context.Entry(course.ToCourse()).State = EntityState.Modified;
                    }
                    else
                        _context.Courses.Add(course.ToCourse());
                }

                await _context.SaveChangesAsync();

                return failedToAddCourses;
            }
            catch (Exception ex)
            {
                throw new CourseCommandException(ex.Message);
            }
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
                _context.Entry(course.ToCourse()).State = EntityState.Modified;
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
                _context.Entry(course).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        bool CourseExists(int crn)
        {
            return (_context.Courses?.Any(e => e.Crn == crn)).GetValueOrDefault();
        }
    }
}
