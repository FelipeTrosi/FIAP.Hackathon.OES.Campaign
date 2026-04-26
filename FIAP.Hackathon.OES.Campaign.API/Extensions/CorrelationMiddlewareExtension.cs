
using FIAP.Hackathon.OES.Campaign.API.Middlewares;

namespace FIAP.Hackathon.OES.Campaign.API.Extensions;

public static class CorrelationMiddlewareExtension
{
    public static IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationMiddleware>();
    }
}
