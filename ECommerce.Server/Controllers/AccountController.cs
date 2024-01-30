using Microsoft.AspNetCore.Mvc;
using ECommerce.Server.Models.DTOs.Requests;
using ECommerce.Server.Services.Interfaces;

namespace ECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }
            var result = await _authService.AuthenticateAsync(model.Username, model.Password);
            if (result.Status != StatusCodes.Status401Unauthorized)
            {
                return Ok(result);
            }
            return Unauthorized(new { errors = result.Error });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
            {
                return BadRequest();
            }
            var refreshTokenResponse = _authService.RefreshToken(refreshTokenRequest.RefreshToken);
            return Ok(refreshTokenResponse);
        }
    }
}
