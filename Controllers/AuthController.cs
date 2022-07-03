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
        public async Task<IActionResult> Register(ProfileRegistrationDTO user)
        {
            var authResponse = await _auth.RegisterAsync(user);
            
            if(!authResponse.IsSuccessful)
            {
                return BadRequest(new AuthFailResponse
                {
                    ErrorMessages = authResponse.ErrorMessages
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost("auth/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ProfileLoginDTO user)
        {
            var authResponse = await _auth.LoginAsync(user);
            
            if(!authResponse.IsSuccessful)
            {
                return BadRequest(new AuthFailResponse
                {
                    ErrorMessages = authResponse.ErrorMessages
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }        
        
    }
}
