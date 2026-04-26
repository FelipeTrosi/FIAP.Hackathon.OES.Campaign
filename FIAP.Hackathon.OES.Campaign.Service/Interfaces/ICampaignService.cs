using FIAP.Hackathon.OES.Campaign.Service.Dto.User;
using FIAP.Hackathon.OES.User.Service.Dto.User;

namespace FIAP.Hackathon.OES.Campaign.Service.Interfaces;

public interface ICampaignService
{
    ICollection<CampaignOutputDto> GetAll();
    CampaignOutputDto? GetById(long id);
    Task Create(CampaignCreateDto entity);
    void Update(CampaignUpdateDto entity);
    void DeleteById(long id);
}
