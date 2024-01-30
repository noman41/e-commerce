using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.Server.Services.Helpers
{
    public static class UserHelper
    {
        public static bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (handler.ReadToken(token) is not JwtSecurityToken jsonToken || jsonToken.ValidTo < DateTime.UtcNow)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
