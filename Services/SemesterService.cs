﻿using CompanionApp.Models;
using CompanionApp.Extensions;
using CompanionApp.ModelsDTO;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.SemesterExceptions;
using FluentValidation;
using EntityFramework.Exceptions.Common;

namespace CompanionApp.Services
{
    public class SemesterService : ISemesterService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Semester>       _dbSet;
        readonly SemesterValidation    _semesterValidation;

        public SemesterService(CompanionAppDBContext context, SemesterValidation semesterValidation)
        {
            _context            = context;
            _dbSet              = context.Semesters;
            _semesterValidation = semesterValidation;
        }

        public async Task<IEnumerable<SemesterDTO>> GetSemestersAsync  ()
        {
            IEnumerable<SemesterDTO> semesters = await _dbSet.Select(x => x.ToSemesterDTO()).ToListAsync();
            if (semesters is null)
            {
                throw new NoSemestersFoundException();
            }
            return semesters;
        }
        public async Task<SemesterDTO>              GetSemesterAsync   (string semesterId)
        {
            Semester? semester = await _dbSet.FindAsync(semesterId);
            if (semester is null)
            {
                throw new SemesterNotFoundException();
            }
            return semester.ToSemesterDTO();
        }
        public async Task                           AddSemesterAsync   (SemesterDTO semester)
        {
            try
            {
                await _semesterValidation.ValidateAndThrowAsync(semester);
                _dbSet.Add(semester.ToSemester());
                await _context.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                throw new SemesterAlreadyExistsException();
            }
            catch (ValidationException ex)
            {
                throw new SemesterCommandException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<SemesterDTO>> AddSemestersAsync  (IEnumerable<SemesterDTO> semesters)
        {
            IList<SemesterDTO> failedToAddSemesters = new List<SemesterDTO>();
            foreach (SemesterDTO semester in semesters)
            {
                try
                {
                    if (!(await _semesterValidation.ValidateAsync(semester)).IsValid)
                    {
                        failedToAddSemesters.Add(semester);
                    }
                    else if (await _dbSet.SemesterExists(semester.Id))
                    {
                        _dbSet.Update(semester.ToSemester());
                    }
                    else
                    {
                        _dbSet.Add(semester.ToSemester());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();
            return failedToAddSemesters;
        }
        public async Task                           DeleteSemesterAsync(string semesterId)
        {
            try
            {
                if (!await _dbSet.SemesterExists(semesterId))
                {
                    throw new SemesterNotFoundException();
                }
                _dbSet.Remove(new Semester() { Id = semesterId });
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}