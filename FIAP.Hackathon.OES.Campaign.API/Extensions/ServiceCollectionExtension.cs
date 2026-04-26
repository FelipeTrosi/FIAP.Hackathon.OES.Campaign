using FIAP.Hackathon.OES.Campaign.Infra.CorrelationId;

namespace FIAP.Hackathon.OES.Campaign.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCorrelationIdGenerator(this IServiceCollection services)
    {
        services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();

        return services;
    }
}
