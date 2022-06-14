using CompanionApp.Exceptions.ProfileExceptions;
using CompanionApp.Extensions;
using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        public IProfileRespository profileRespository { get; }
        public ProfilesController(IProfileRespository ProfileRespository)
        {
            profileRespository = ProfileRespository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileQuerryDTO>> GetProfile(Guid id)
        {
            try
            {
                ProfileQuerryDTO profile = await profileRespository.GetProfile(id);
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
                await profileRespository.EditProfile(id, profile);
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
                return await profileRespository.CreateProfile(profile);
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
                await profileRespository.DeleteProfile(id);
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
