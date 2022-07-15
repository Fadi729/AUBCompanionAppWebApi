using CompanionApp.ModelsDTO;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        readonly IUserService _auth;

        public AuthController(IUserService auth)
        {
            _auth = auth;
        }

        [HttpPost("auth/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(ProfileRegistrationDTO user, CancellationToken cancellationToken)
        {
            return Ok(await _auth.RegisterAsync(user, cancellationToken));
        }

        [HttpPost("auth/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login   (ProfileLoginDTO user,        CancellationToken cancellationToken)
        {
            return Ok(await _auth.LoginAsync(user, cancellationToken));
        }        
        
    }
}
