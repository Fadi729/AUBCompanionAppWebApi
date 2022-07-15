using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.AspNetCore.Mvc;
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
        readonly IUserService    _userManager;

        public ProfilesController(IProfileService profileService, IUserService userManager)
        {
            _profileService = profileService;
            _userManager    = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileQueryDTO>> GetProfile   (Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _profileService.GetProfileAsync(id, cancellationToken));
        }

        [HttpDelete]
        public async Task<IActionResult>                 DeleteProfile(CancellationToken cancellationToken)
        {
            await _userManager.DeleteAsync(HttpContext.GetUserID(), cancellationToken);
            return NoContent();
        }
    }
}
