using FIAP.Hackathon.OES.Campaign.Domain.Entity;

namespace FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;

public interface ICampaignRepository : IRepository<CampaignEntity>
{
    IEnumerable<CampaignEntity> GetActiveCampaigns();
    CampaignEntity UpdateCampaignDoanatedValue(long campaignId, decimal value);
}
