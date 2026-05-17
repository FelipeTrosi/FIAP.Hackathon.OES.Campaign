using FIAP.Hackathon.OES.Campaign.Domain.Entity.Abstracts;

namespace FIAP.Hackathon.OES.Campaign.Service.Dto.User;

public class CampaignActiveOutputDto : EntityBase
{
    public string Title { get; set; } = null!;
    public decimal FinancialGoal { get; set; }
    public decimal TotalDonationsCollected { get; set; }
}
