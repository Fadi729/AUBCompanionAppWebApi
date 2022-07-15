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

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<CourseDTO> coursesDTOs = await _dbSet.Select(c => c.ToCourseDTO()).ToListAsync(cancellationToken);
            if (!coursesDTOs.Any())
            {
                throw new NoCoursesFoundException();
            }
            return coursesDTOs;
        }
        public async Task<CourseDTO>              GetCourseAsync    (int crn,          CancellationToken cancellationToken)
        {
            Course? course = await _dbSet.FindAsync(new object?[] { crn }, cancellationToken);
            if (course is null)
            {
                throw new CourseNotFoundException();
            }
            return course.ToCourseDTO();
        }
        public async Task<CourseDTO>              AddCourseAsync    (CourseDTO course, CancellationToken cancellationToken)
        {
            #region try block
            try
            {
                await _courseValidation.ValidateAndThrowAsync(course, cancellationToken);
                Course newCourse = course.ToCourse();
                _dbSet.Add(newCourse);
                await _context.SaveChangesAsync(cancellationToken);
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
        public async Task<IEnumerable<CourseDTO>> AddCoursesAsync   (IEnumerable<CourseDTO> courses, CancellationToken cancellationToken)
        {
            IList<CourseDTO> failedToAddCourses = new List<CourseDTO>();
            #region try   block
            try
            {
                foreach (CourseDTO course in courses)
                {
                    if (!(await _courseValidation.ValidateAsync(course, cancellationToken)).IsValid)
                    {
                        failedToAddCourses.Add(course);
                    }
                    else if (await _dbSet.CourseExists(course.Crn, cancellationToken))
                    {
                        _dbSet.Update(course.ToCourse());
                    }
                    else
                        _dbSet.Add(course.ToCourse());
                }

                await _context.SaveChangesAsync(cancellationToken);

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
        public async Task                         EditCourseAsync   (CourseDTO course, CancellationToken cancellationToken)
        {
            await _courseValidation.ValidateAndThrowAsync(course,cancellationToken);
            if (!await _dbSet.CourseExists(course.Crn, cancellationToken))
            {
                throw new CourseNotFoundException();
            }
            _dbSet.Update(course.ToCourse());
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task                         DeleteCourseAsync (int crn,          CancellationToken cancellationToken)
        {
            try
            {
                if (!await _dbSet.CourseExists(crn.ToString(), cancellationToken))
                {
                    throw new CourseNotFoundException();
                }
                _dbSet.Remove(new Course() { Crn = crn });
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}