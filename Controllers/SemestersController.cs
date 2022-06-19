using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.SemesterExceptions;

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
            try
            {
                return Ok(await _semesterService.GetSemestersAsync());
            }
            catch (NoSemestersFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterDTO>>              GetSemester   (string id)
        {
            try
            {
                return Ok(await _semesterService.GetSemesterAsync(id));
            }
            catch (SemesterNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("single")]
        public async Task<ActionResult<SemesterDTO>>              PostSemester  (SemesterDTO semesterDTO)
        {
            try
            {
                await _semesterService.AddSemesterAsync(semesterDTO);
                return CreatedAtAction("GetSemester", new { id = semesterDTO.Id }, semesterDTO);
            }
            catch (Exception ex) when (ex is SemesterAlreadyExistsException || ex is SemesterCommandException)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                throw;
            }

        }

        [HttpPost("many")]
        public async Task<ActionResult<SemesterDTO>>              PostSemesters (IEnumerable<SemesterDTO> semesters)
        {
            try
            {
                return Ok(await _semesterService.AddSemestersAsync(semesters));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>                          DeleteSemester(string id)
        {
            try
            {
                await _semesterService.DeleteSemesterAsync(id);
                return NoContent();
            }
            catch(SemesterNotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}