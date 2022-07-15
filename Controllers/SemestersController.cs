using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SemestersController : ControllerBase
    {
        readonly ISemesterService _semesterService;

        public SemestersController(ISemesterService SemesterServcie)
        {
            _semesterService = SemesterServcie;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> GetSemesters  (CancellationToken cancellationToken)
        {
            return Ok(await _semesterService.GetSemestersAsync(cancellationToken));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterDTO>>              GetSemester   (string id, CancellationToken cancellationToken)
        {
            return Ok(await _semesterService.GetSemesterAsync(id, cancellationToken));
        }
        
        [HttpPost("single")]
        public async Task<ActionResult<SemesterDTO>>              PostSemester  (SemesterDTO semesterDTO, CancellationToken cancellationToken)
        {
            await _semesterService.AddSemesterAsync(semesterDTO, cancellationToken);
            return CreatedAtAction("GetSemester", new { id = semesterDTO.Id }, semesterDTO);
        }
        
        [HttpPost("many")]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> PostSemesters (IEnumerable<SemesterDTO> semesters, CancellationToken cancellationToken)
        {
            return Ok(await _semesterService.AddSemestersAsync(semesters, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>                          DeleteSemester(string id, CancellationToken cancellationToken)
        {
            await _semesterService.DeleteSemesterAsync(id, cancellationToken);
            return NoContent();
        }
    }
}