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
        readonly IUserService _userService;

        public ProfilesController(IUserService userService)
        {
            _userService    = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileQueryDTO>> GetProfile   (Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetProfileAsync(id, cancellationToken));
        }

        [HttpDelete]
        public async Task<IActionResult>                 DeleteProfile(CancellationToken cancellationToken)
        {
            await _userService.DeleteProfileAsync(HttpContext.GetUserID(), cancellationToken);
            return NoContent();
        }
    }
}
