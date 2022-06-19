using CompanionApp.ModelsDTO;

namespace CompanionApp.Services.Contracts
{
    public interface ISemesterService
    {
        public Task<IEnumerable<SemesterDTO>> GetSemestersAsync  ();
        public Task<SemesterDTO>              GetSemesterAsync   (string semesterId);
        public Task                           AddSemesterAsync   (SemesterDTO semester);
        public Task<IEnumerable<SemesterDTO>> AddSemestersAsync  (IEnumerable<SemesterDTO> semester);
        public Task                           DeleteSemesterAsync(string semesterId);
    }
}
