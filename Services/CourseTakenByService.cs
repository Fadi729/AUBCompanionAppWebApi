﻿using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Validation;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
using CompanionApp.Exceptions.CourseExceptions;
using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Exceptions.SemesterExceptions;
using CompanionApp.Exceptions.CourseTakenByExceptions;

namespace CompanionApp.Services
{
    public class CourseTakenByService : ICourseTakenByService
    {
        readonly CompanionAppDBContext   _context;
        readonly DbSet<CourseTakenBy>    _dbSetCourseTakenBy;
        readonly DbSet<Course>           _dbSetCourse;
        readonly DbSet<Semester>         _dbSetSemester;
        readonly DbSet<Profile>          _dbSetProfile;
        readonly CourseTakenByValidation _courseTakenByValidation;

        public CourseTakenByService(CompanionAppDBContext context, CourseTakenByValidation validation)
        {
            _context                 = context;
            _dbSetCourseTakenBy      = _context.CourseTakenBy;
            _dbSetCourse             = _context.Courses;
            _dbSetSemester           = _context.Semesters;
            _dbSetProfile            = _context.Profiles;
            _courseTakenByValidation = validation;
        }

        public async Task<IEnumerable<CourseTakenBy_Course_DTO>> GetUsersTakingCourse           (int crn)
        {
            if (!await _dbSetCourse.CourseExists(crn.ToString()))
            {
                throw new CourseNotFoundException();
            }

            IEnumerable<CourseTakenBy_Course_DTO> courseTakenBy = await _dbSetCourseTakenBy
                .Include(c => c.User)
                .Include(y => y.Semester)
                .Where  (x => x.CCrn == crn)
                .Select (x => x.ToCourseTakenBy_Course_DTO())
                .ToListAsync();

            if (!courseTakenBy.Any())
            {
                throw new CourseNotTakenByAnyUserException();
            }

            return courseTakenBy;
        }
        public async Task<IEnumerable<CourseTakenBy_User_DTO>>   GetCoursesTakenByUser          (Guid userID)
        {
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }

            IEnumerable<CourseTakenBy_User_DTO> coursesTakenByUser = await _dbSetCourseTakenBy
                .Include(c => c.CCrnNavigation)
                .Include(y => y.Semester)
                .Where  (x => x.UserId == userID)
                .Select (x => x.ToCourseTakenBy_User_DTO())
                .ToListAsync();

            if (!coursesTakenByUser.Any())
            {
                throw new NoCoursesTakenByUserException();
            }

            return coursesTakenByUser;
        }
        public async Task<IEnumerable<CourseTakenBy_User_DTO>>   GetCoursesTakenByUserInSemester(Guid userID, string semesterID)
        {
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetSemester.SemesterExists(semesterID))
            {
                throw new SemesterNotFoundException();
            }

            IEnumerable<CourseTakenBy_User_DTO> coursesTakenByUser = await _dbSetCourseTakenBy
                .Include(c => c.CCrnNavigation)
                .Include(y => y.Semester)
                .Where  (x => x.UserId == userID && x.SemesterId == semesterID)
                .Select (x => x.ToCourseTakenBy_User_DTO())
                .ToListAsync();

            if (!coursesTakenByUser.Any())
            {
                throw new NoCoursesTakenByUserException();
            }
            
            return coursesTakenByUser;
        }
        public async Task<CourseTakenBy_POST_DTO>                AddCourseToUser                (CourseTakenBy_POST_DTO courseTakenBy)
        {
            try
            {
                await _courseTakenByValidation.ValidateAndThrowAsync(courseTakenBy);

                if (!await _dbSetProfile.ProfileExists(courseTakenBy.UserId))
                {
                    throw new ProfileNotFoundException();
                }
                if (!await _dbSetCourse.CourseExists(courseTakenBy.CCrn))
                {
                    throw new CourseNotFoundException();
                }
                if (!await _dbSetSemester.SemesterExists(courseTakenBy.SemesterId))
                {
                    throw new SemesterNotFoundException();
                }
                if (!await _dbSetCourse.CourseGivenInSemester(courseTakenBy.CCrn, courseTakenBy.SemesterId))
                {
                    throw new CourseNotGivenInSemesterException();
                }
                CourseTakenBy newCourseTakenBy = courseTakenBy.ToCourseTakenBy();

                _dbSetCourseTakenBy.Add(newCourseTakenBy);
                await _context.SaveChangesAsync();

                return courseTakenBy;
            }
            catch (UniqueConstraintException)
            {
                throw new CourseAlreadyTakenByUserException();
            }
        }
        public async Task                                        DeleteCoursesTakenByUser       (Guid userID, int crn, string semesterID)
        {
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileNotFoundException();
            }
            if (!await _dbSetCourse.CourseExists(crn.ToString()))
            {
                throw new CourseNotFoundException();
            }
            if (!await _dbSetSemester.SemesterExists(semesterID))
            {
                throw new SemesterNotFoundException();
            }

            CourseTakenBy? courseTakenBy = await _dbSetCourseTakenBy.GetCourseTakenByAsync(userID, crn, semesterID);

            if(courseTakenBy is null)
            {
                throw new CourseNotTakenByUserException();
            }
            if(!DataOperations.ProfileTookCourse(courseTakenBy.UserId, userID))
            {
                throw new UserDoesNotOwnCourseTakenByRelation();
            }

            _dbSetCourseTakenBy.Remove(courseTakenBy);
            await _context.SaveChangesAsync();
        }
    }
}
