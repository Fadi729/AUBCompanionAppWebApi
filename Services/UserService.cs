using System.Text;
using CompanionApp.Jwt;
using CompanionApp.Models;
using System.Security.Claims;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<AuthResponse> RegisterAsync               (ProfileRegistrationDTO user)
        {
            await _profileService.ValidateProfile(user.ToProfileCommandDTO());

            var profile = await _userManager.FindByEmailAsync(user.Email);

            if (profile is not null)
            {
                return new AuthResponse
                {
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


            var createProfile = await _userManager.CreateAsync(newProfile, user.Password);

            if (!createProfile.Succeeded)
            {
                return new AuthResponse
                {
                    ErrorMessages = createProfile.Errors.Select(e => e.Description)
                };
            }

            return AuthenticationTokenGenerator(newProfile);
        }
        public async Task<AuthResponse> LoginAsync                  (ProfileLoginDTO user)
        {
            var profile = await _userManager.FindByEmailAsync(user.Email);

            if (profile is null)
            {
                return new AuthResponse
                {
                    ErrorMessages = new[] { "Incorrect Credentials" }
                };
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(profile, user.Password);
            if (!isValidPassword)
            {
                return new AuthResponse
                {
                    ErrorMessages = new[] { "Incorrect Credentials" }
                };
            }

            return AuthenticationTokenGenerator(profile);
        }
        public async Task               DeleteAsync                 (Guid userID)
        {
            var profile = await _userManager.FindByIdAsync(userID.ToString());

            if (profile is null)
            {
                throw new ProfileNotFoundException();
            }

            var result = await _userManager.DeleteAsync(profile);
            
            if(!result.Succeeded)
            {
                return;
            }

            await _profileService.DeleteProfileAsync(userID);
        }
        private      AuthResponse       AuthenticationTokenGenerator(Profile newProfile)
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
                IsSuccessful = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
