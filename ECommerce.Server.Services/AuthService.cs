using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ECommerce.Server.Models.DataModels;
using ECommerce.Server.Models.DTOs.Responses;
using ECommerce.Server.Repositories.Interfaces;
using ECommerce.Server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Server.Services
{
    public class AuthService(UserManager<POSUser> userManager, IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        private readonly UserManager<POSUser> _userManager = userManager;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<LoginResponse> AuthenticateAsync(string username, string password)
        {
            var loginResponse = new LoginResponse();
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(username);
                if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                {
                    loginResponse.Error = "Invalid username or password";
                    loginResponse.Status = StatusCodes.Status401Unauthorized;
                    return loginResponse;
                }
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.UserName)
                };

                //var roles = await _userManager.GetRolesAsync(user);
                //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var accessTokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Issuer"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = credentials,
                };
                var refreshTokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Issuer"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = credentials,
                };

                var accessToken = new JwtSecurityTokenHandler().CreateToken(accessTokenDescriptor);
                var refreshToken = new JwtSecurityTokenHandler().CreateToken(refreshTokenDescriptor);
                loginResponse.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
                loginResponse.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                loginResponse.UserDetails = user;
                loginResponse.Status = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                loginResponse.Error = ex.Message;
                loginResponse.Status = StatusCodes.Status500InternalServerError;
            }
            return loginResponse;
        }

        public RefreshTokenResponse RefreshToken(string refreshToken)
        {
            var refreshTokenResponse = new RefreshTokenResponse();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Issuer"]
                };
                var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out _);
                var newAccessToken = GenerateToken(principal, DateTime.UtcNow.AddMinutes(1));
                var newRefreshToken = GenerateToken(principal, DateTime.UtcNow.AddMinutes(10));
                refreshTokenResponse.AccessToken = newAccessToken;
                refreshTokenResponse.RefreshToken = newRefreshToken;
                refreshTokenResponse.Status = StatusCodes.Status200OK;
            }
            catch (SecurityTokenValidationException ex)
            {
                refreshTokenResponse.Error = ex.Message;
                refreshTokenResponse.Status = StatusCodes.Status401Unauthorized;
            }
            catch (Exception ex)
            {
                refreshTokenResponse.Error = ex.Message;
                refreshTokenResponse.Status = StatusCodes.Status500InternalServerError;
            }
            return refreshTokenResponse;
        }

        private string GenerateToken(ClaimsPrincipal principal, DateTime dateTime)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = principal.Identities.First(),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"],
                Expires = dateTime,
                SigningCredentials = creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var newToken = tokenHandler.WriteToken(token);
            return newToken;
        }
    }
}
