using FIAP.Hackathon.OES.Campaign.Domain.Entity.Abstracts;
using FIAP.Hackathon.OES.Campaign.Domain.Enums.Campaign;

namespace FIAP.Hackathon.OES.User.Service.Dto.User;

public class CampaignUpdateDto : EntityBase
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartedDateTime { get; set; }
    public DateTime? FinishedDateTime { get; set; }
    public decimal FinancialGoal { get; set; }
    public StatusCampaignEnum Status { get; set; }
}

