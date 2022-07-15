using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ISemesterService
    {
        public Task<IEnumerable<SemesterDTO>> GetSemestersAsync  (                                   CancellationToken cancellationToken);
        public Task<SemesterDTO>              GetSemesterAsync   (string semesterId                , CancellationToken cancellationToken);
        public Task                           AddSemesterAsync   (SemesterDTO semester             , CancellationToken cancellationToken);
        public Task<IEnumerable<SemesterDTO>> AddSemestersAsync  (IEnumerable<SemesterDTO> semester, CancellationToken cancellationToken);
        public Task                           DeleteSemesterAsync(string semesterId                , CancellationToken cancellationToken);
    }
}