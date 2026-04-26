using FIAP.Hackathon.OES.Campaign.Infra.Logger;
using FIAP.Hackathon.OES.Campaign.Service.Interfaces;
using FIAP.Hackathon.OES.Campaign.Service.Services;

namespace FIAP.Hackathon.OES.Campaign.API.Extensions; 

public static class ServiceDIExtension
{
    public static IServiceCollection AddServiceDI(this IServiceCollection services)
    {
        services.AddTransient(typeof(IBaseLogger<>), typeof(BaseLogger<>));

        services.AddTransient<ICampaignService, CampaignService>();

        return services;
    }
}
