using ECommerce.Server.Models.DataModels;

namespace ECommerce.Server.Models.DTOs.Responses
{
    public class LoginResponse : BaseResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public POSUser? UserDetails { get; set; }
    }
}
