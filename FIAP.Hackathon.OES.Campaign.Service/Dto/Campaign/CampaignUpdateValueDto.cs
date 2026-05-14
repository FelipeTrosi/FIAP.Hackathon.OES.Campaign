using FIAP.Hackathon.OES.Campaign.Domain.Entity.Abstracts;
using FIAP.Hackathon.OES.Campaign.Domain.Enums.Campaign;

namespace FIAP.Hackathon.OES.User.Service.Dto.User;

public class CampaignUpdateValueDto : EntityBase
{
    public long CampaignId { get; set; }
    public decimal Value { get; set; }
}

