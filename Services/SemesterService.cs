using FluentValidation;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Validation;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
using CompanionApp.Exceptions.SemesterExceptions;

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

        public async Task<IEnumerable<SemesterDTO>> GetSemestersAsync  (CancellationToken cancellationToken)
        {
            IEnumerable<SemesterDTO> semesters = await _dbSet.Select(x => x.ToSemesterDTO()).ToListAsync(cancellationToken);
            if (!semesters.Any())
            {
                throw new NoSemestersFoundException();
            }

            return semesters;
        }

        public async Task<SemesterDTO>              GetSemesterAsync   (string semesterId,    CancellationToken cancellationToken)
        {
            Semester? semester = await _dbSet.FindAsync(new object?[] { semesterId }, cancellationToken: cancellationToken);
            if (semester is null)
            {
                throw new SemesterNotFoundException();
            }

            return semester.ToSemesterDTO();
        }

        public async Task                           AddSemesterAsync   (SemesterDTO semester, CancellationToken cancellationToken)
        {
            try
            {
                await _semesterValidation.ValidateAndThrowAsync(semester, cancellationToken);
                _dbSet.Add(semester.ToSemester());
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (UniqueConstraintException)
            {
                throw new SemesterAlreadyExistsException();
            }
        }

        public async Task<IEnumerable<SemesterDTO>> AddSemestersAsync  (IEnumerable<SemesterDTO> semesters, CancellationToken cancellationToken)
        {
            IList<SemesterDTO> failedToAddSemesters = new List<SemesterDTO>();
            foreach (SemesterDTO semester in semesters)
            {
                try
                {
                    if (!(await _semesterValidation.ValidateAsync(semester, cancellationToken)).IsValid)
                    {
                        failedToAddSemesters.Add(semester);
                    }
                    else if (await _dbSet.SemesterExists(semester.Id, cancellationToken))
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

            await _context.SaveChangesAsync(cancellationToken);
            return failedToAddSemesters;
        }

        public async Task                           DeleteSemesterAsync(string semesterId, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _dbSet.SemesterExists(semesterId, cancellationToken))
                {
                    throw new SemesterNotFoundException();
                }

                _dbSet.Remove(new Semester() { Id = semesterId });
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}