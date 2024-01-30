using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Server.Filters
{
    public class PoSExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.HttpContext.Response.WriteAsync(context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}
