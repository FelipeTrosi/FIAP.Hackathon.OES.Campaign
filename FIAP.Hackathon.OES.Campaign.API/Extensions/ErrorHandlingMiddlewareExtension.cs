using FIAP.Hackathon.OES.Campaign.API.Middleware;

namespace FIAP.Hackathon.OES.Campaign.API.Extensions;

public static class ErrorHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
