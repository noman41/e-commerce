namespace ECommerce.Server.Models.DTOs.Responses
{
    public class RefreshTokenResponse : BaseResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
