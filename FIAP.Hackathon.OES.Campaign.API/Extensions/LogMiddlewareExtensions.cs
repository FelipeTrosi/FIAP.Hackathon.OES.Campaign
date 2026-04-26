
using FIAP.Hackathon.OES.Campaign.API.Middleware;

namespace FIAP.Hackathon.OES.Campaign.API.Extensions;

public static class LogMiddlewareExtensions
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<LogMiddleware>();

}