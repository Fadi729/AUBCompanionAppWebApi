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
            _context          = DBcontext;
            _courseValidation = validation;
        }
        
        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            IEnumerable<CourseDTO> courseDTOs =  await _context.Courses.Select(c => c.ToCourseDTO()).ToListAsync();
            if (courseDTOs is null)
            {
                throw new NoCoursesFoundException();
            }
            return courseDTOs;
        }
        public async Task<CourseDTO> GetCourseAsync(int crn)
        {
            Course? course = await _context.Courses.FindAsync(crn);
            if (course is null)
            {
                throw new CourseNotFoundException();
            }
            return course.ToCourseDTO();
        }

        public async Task<CourseDTO> AddCourseAsync(CourseDTO course)
        {
            #region try block
            try
            {
                System.Text.RegularExpressions.Regex subjectrx = new(@"\b[A-Z]{4}\b");
                Console.WriteLine(course.Subject);
                Console.WriteLine(subjectrx.IsMatch(course.Subject));
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

        public Task AddCoursesAsync(IEnumerable<CourseDTO> courses)
        {
            throw new NotImplementedException();
        }

        public async Task EditCourseAsync(int crn, CourseDTO course)
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

        public Task DeleteCourseAsync(int crn)
        {
            throw new NotImplementedException();
        }

        bool CourseExists(int crn)
        {
            return (_context.Courses?.Any(e => e.Crn == crn)).GetValueOrDefault();
        }        
    }
}
