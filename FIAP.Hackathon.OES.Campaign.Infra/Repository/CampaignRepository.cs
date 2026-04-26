using FIAP.Hackathon.OES.Campaign.Domain.Entity;
using FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;
using FIAP.Hackathon.OES.User.Infra.Repository;

namespace FIAP.Hackathon.OES.Campaign.Infra.Repository
{
    public class CampaignRepository : EFRepository<CampaignEntity>, ICampaignRepository
    {
        public CampaignRepository(ApplicationDbContext context) : base(context)
        {
        }
        
    }
}
