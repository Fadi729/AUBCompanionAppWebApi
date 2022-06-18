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
        IProfileService ProfileRespository { get; init;  }
        public ProfilesController(IProfileService ProfileRespository)
        {
            this.ProfileRespository = ProfileRespository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileQuerryDTO>> GetProfile(Guid id)
        {
            try
            {
                ProfileQuerryDTO profile = await ProfileRespository.GetProfileAsync(id);
                return profile;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(Guid id, ProfileCommandDTO profile)
        {
            try
            {
                await ProfileRespository.EditProfileAsync(id, profile);
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

        [HttpPost]
        public async Task<ActionResult<ProfileQuerryDTO>> PostProfile(ProfileCommandDTO profile)
        {
            try
            {
                return await ProfileRespository.CreateProfileAsync(profile);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            try
            {
                await ProfileRespository.DeleteProfileAsync(id);
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
