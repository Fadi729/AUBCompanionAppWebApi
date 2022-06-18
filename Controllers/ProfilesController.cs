using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Extensions;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        readonly IProfileService _profileService;
        public ProfilesController(IProfileService _profileService)
        {
            this._profileService = _profileService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileQueryDTO>> GetProfile   (Guid id)
        {
            try
            {
                return Ok(await _profileService.GetProfileAsync(id));
            }
            catch (ProfileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<ProfileQueryDTO>> PostProfile  (ProfileCommandDTO profile)
        {
            try
            {
                ProfileQueryDTO profileDTO = await _profileService.CreateProfileAsync(profile);
                return CreatedAtAction("GetProfile", new { id = profileDTO.Id }, profileDTO);
            }
            catch (ProfileAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProfileCommandException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult>                 PutProfile   (Guid id, ProfileCommandDTO profile)
        {
            try
            {
                await _profileService.EditProfileAsync(id, profile);
                return NoContent();
            }
            catch (ProfileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProfileCommandException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>                 DeleteProfile(Guid id)
        {
            try
            {
                await _profileService.DeleteProfileAsync(id);
                return NoContent();
            }
            catch (ProfileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
