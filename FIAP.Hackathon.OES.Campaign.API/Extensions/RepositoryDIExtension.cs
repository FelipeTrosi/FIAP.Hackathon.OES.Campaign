
using FIAP.Hackathon.OES.Campaign.Infra.Repository;
using FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;

namespace FIAP.Hackathon.OES.Campaign.API.Extensions
{
    public static class RepositoryDIExtension
    {
        public static IServiceCollection AddRepositoryDI(this IServiceCollection services)
        {
            services.AddScoped<ICampaignRepository, CampaignRepository>();

            return services;
        }
    }
}
