using ECommerce.Server.Models.DTOs.Responses;

namespace ECommerce.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthenticateAsync(string username, string password);
        RefreshTokenResponse RefreshToken(string accessToken);
    }
}
