using ECommerce.Server.Services.Helpers;

namespace ECommerce.Server.Middlewares
{
    public class TokenCheckingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", string.Empty);
            if (context.Request.Path.StartsWithSegments("/api/Account/login") || context.Request.Path.StartsWithSegments("/api/Account/RefreshToken"))
            {
                await _next(context);
                return;
            }
            if (!string.IsNullOrEmpty(token))
            {
                bool isExpired = UserHelper.IsTokenExpired(token);
                if (isExpired)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            await _next(context);
        }
    }
}
