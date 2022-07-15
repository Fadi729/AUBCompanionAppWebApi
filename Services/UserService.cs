using System.Text;
using CompanionApp.Jwt;
using CompanionApp.Models;
using System.Security.Claims;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CompanionApp.Exceptions;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Profile> _userManager;
        readonly JwtSettings          _jwtSettings;
        readonly IProfileService      _profileService;
        public UserService(UserManager<Profile> userManager, JwtSettings jwtSettings, IProfileService profileService)
        {
            _userManager    = userManager;
            _jwtSettings    = jwtSettings;
            _profileService = profileService;
        }

        public async Task<AuthResponse> RegisterAsync(ProfileRegistrationDTO user, CancellationToken cancellationToken)
        {
            await _profileService.ValidateProfile(user, cancellationToken);
            
            Profile? profile = await _userManager.FindByEmailAsync(user.Email);

            if (profile is not null)
            {
                throw new AuthException
                {
                    ErrorCode     = (int)System.Net.HttpStatusCode.Conflict,
                    ErrorMessages = new[] { "User with this email address already exists" }
                };
            }

            Profile newProfile = new()
            {
                Id        = Guid.NewGuid(),
                Email     = user.Email,
                FirstName = user.FirstName,
                LastName  = user.LastName,
                Major     = user.Major,
                Class     = user.Class,
                UserName  = user.Username
            };

            IdentityResult? createProfile = await _userManager.CreateAsync(newProfile, user.Password);

            if (!createProfile.Succeeded)
            {
                throw new AuthException
                {
                    ErrorCode     = (int)System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = createProfile.Errors.Select(e => e.Description)
                };
            }

            return AuthenticationTokenGenerator(newProfile);
        }
        public async Task<AuthResponse> LoginAsync   (ProfileLoginDTO user,        CancellationToken cancellationToken)
        {
            Profile? profile = await _userManager.FindByEmailAsync(user.Email);

            if (profile is null)
            {
                throw new AuthException
                {
                    ErrorCode     = (int)System.Net.HttpStatusCode.Unauthorized,
                    ErrorMessages = new[] { "Invalid Credentials" }
                };
            }

            bool isValidPassword = await _userManager.CheckPasswordAsync(profile, user.Password);
            if (!isValidPassword)
            {
                throw new AuthException
                {
                    ErrorCode     = (int)System.Net.HttpStatusCode.Unauthorized,
                    ErrorMessages = new[] { "Invalid Credentials" }
                };
            }

            return AuthenticationTokenGenerator(profile);
        }
        public async Task               DeleteAsync  (Guid userID,                 CancellationToken cancellationToken)
        {
            Profile? profile = await _userManager.FindByIdAsync(userID.ToString());

            if (profile is null)
            {
                throw new ProfileNotFoundException();
            }

            await _userManager.DeleteAsync(profile);
        }

        AuthResponse AuthenticationTokenGenerator(Profile newProfile)
        {
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userID"                            , newProfile.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email       , newProfile.Email),
                    new Claim(JwtRegisteredClaimNames.Name        , newProfile.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName  , newProfile.LastName),
                    new Claim("Major"                             , newProfile.Major is not null ? newProfile.Major : string.Empty),
                    new Claim("Class"                             , newProfile.Class is not null ? newProfile.Class : string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti         , Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
