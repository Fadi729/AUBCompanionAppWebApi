using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfilesController : ControllerBase
    {
        readonly IProfileService _profileService;
        readonly IUserManager    _userManager;

        public ProfilesController(IProfileService profileService, IUserManager userManager)
        {
            _profileService = profileService;
            _userManager    = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileQueryDTO>> GetProfile   (Guid id)
        {
            return Ok(await _profileService.GetProfileAsync(id));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult>                 PutProfile   (Guid id, ProfileCommandDTO profile)
        //{
        //    await _profileService.EditProfileAsync(id, profile);
        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<IActionResult>                 DeleteProfile()
        {
            await _userManager.DeleteAsync(Guid.Parse(HttpContext.GeUserID()));
            return NoContent();
        }
    }
}
