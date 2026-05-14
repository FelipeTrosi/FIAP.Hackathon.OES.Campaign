using FIAP.Hackathon.OES.Campaign.Domain.Entity;
using FIAP.Hackathon.OES.Campaign.Domain.Enums.Campaign;
using FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;
using FIAP.Hackathon.OES.User.Infra.Repository;

namespace FIAP.Hackathon.OES.Campaign.Infra.Repository
{
    public class CampaignRepository : EFRepository<CampaignEntity>, ICampaignRepository
    {
        public CampaignRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<CampaignEntity> GetActiveCampaigns() => _dbSet.Where(q => q.Status == StatusCampaignEnum.ACTIVE).ToList();

        public CampaignEntity UpdateCampaignDoanatedValue(long campaignId, decimal value)
        {
            var entity = _dbSet.FirstOrDefault(q => q.Id == campaignId);

            if (entity == null) throw new Exception("Usuário não encontrado !");

            entity.TotalDonationsCollected += value;

            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
