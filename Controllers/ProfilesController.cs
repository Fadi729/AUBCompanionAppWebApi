using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;

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
            return Ok(await _profileService.GetProfileAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<ProfileQueryDTO>> PostProfile  (ProfileCommandDTO profile)
        {
            ProfileQueryDTO profileDTO = await _profileService.CreateProfileAsync(profile);
            return CreatedAtAction("GetProfile", new { id = profileDTO.Id }, profileDTO);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult>                 PutProfile   (Guid id, ProfileCommandDTO profile)
        {
            await _profileService.EditProfileAsync(id, profile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>                 DeleteProfile(Guid id)
        {
            await _profileService.DeleteProfileAsync(id);
            return NoContent();
        }
    }
}
