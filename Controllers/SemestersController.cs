using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        readonly ISemesterService _semesterService;

        public SemestersController(ISemesterService SemesterServcie)
        {
            _semesterService = SemesterServcie;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> GetSemesters  ()
        {
            return Ok(await _semesterService.GetSemestersAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterDTO>>              GetSemester   (string id)
        {
            return Ok(await _semesterService.GetSemesterAsync(id));
        }
        
        [HttpPost("single")]
        public async Task<ActionResult<SemesterDTO>>              PostSemester  (SemesterDTO semesterDTO)
        {
            await _semesterService.AddSemesterAsync(semesterDTO);
            return CreatedAtAction("GetSemester", new { id = semesterDTO.Id }, semesterDTO);
        }
        
        [HttpPost("many")]
        public async Task<ActionResult<SemesterDTO>>              PostSemesters (IEnumerable<SemesterDTO> semesters)
        {
            return Ok(await _semesterService.AddSemestersAsync(semesters));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>                          DeleteSemester(string id)
        {
            await _semesterService.DeleteSemesterAsync(id);
            return NoContent();
        }
    }
}