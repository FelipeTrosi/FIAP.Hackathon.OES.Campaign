using FIAP.Hackathon.OES.Campaign.Domain.Enums.Campaign;
using FIAP.Hackathon.OES.Campaign.Infra.Logger;
using FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;
using FIAP.Hackathon.OES.Campaign.Service.Dto.User;
using FIAP.Hackathon.OES.Campaign.Service.Exceptions;
using FIAP.Hackathon.OES.Campaign.Service.Interfaces;
using FIAP.Hackathon.OES.Contracts;
using FIAP.Hackathon.OES.User.Service.Dto.User;
using FIAP.Hackathon.OES.User.Service.Util;
using MassTransit;

namespace FIAP.Hackathon.OES.Campaign.Service.Services;

public class CampaignService(IBaseLogger<CampaignService> logger, ICampaignRepository repository, ISendEndpointProvider send) : ICampaignService
{
    private readonly ICampaignRepository _repository = repository;
    private readonly IBaseLogger<CampaignService> _logger = logger;
    private readonly ISendEndpointProvider _send = send;

    public async Task Create(CampaignCreateDto entity)
    {
        var errors = new Dictionary<string, string[]>();

        _logger.LogInformation("Iniciando serviço 'CREATE' de campanha !");

        if (entity.StartedDateTime < DateTime.Now)
        {
            _logger.LogError("Data de criação inválida");
            errors["StartedDateTime"] = ["Data de criação inválida"];
        }

        if (entity.FinancialGoal <= 0)
        {
            _logger.LogError("Meta financeira deve ser maior que 0");
            errors["FinancialGoal"] = ["Meta financeira deve ser maior que 0"];
        }

        if (errors.Any())
            throw new BadRequestException("Erro de validação", errors);

        var createdCampaign = _repository.Create(new()
        {
            Title = entity.Title,
            CreatedAt = DateTime.Now,
            Description = entity.Description,
            StartedDateTime = entity.StartedDateTime,
            FinishedDateTime = entity.FinishedDateTime,
            FinancialGoal = entity.FinancialGoal,
            Status = entity.Status
        });

        _logger.LogInformation("Campanha cadastrado com sucesso !");

    }

    public async Task CreateDonationAsync(CampaignCreateDonationDto entity)
    {
        _logger.LogInformation("Iniciando serviço 'CREATE' da Doação!");

        var campaign = _repository.GetById(entity.CampaignId);

        if (campaign is null)
        {
            throw new BadRequestException("Campanha não encontrada.",
                new Dictionary<string, string[]>
                {
                    { "CampaignId", new[] { $"Campanha {entity.CampaignId} não encontrada." } }
                });
        }

        if (campaign.Status != StatusCampaignEnum.ACTIVE)
        {
            _logger.LogError($"A doação só pode ser realizada para campanhas ativas! Id da Campanha: {campaign.Id}");

            throw new BadRequestException("A doação só pode ser realizada para campanhas ativas!",
                new Dictionary<string, string[]>
                {
                    { "Status", new[] { $"A campanha possui o Status: {campaign.Status}" } }
                });
        }

        var endpoint = await _send.GetSendEndpoint(new Uri("queue:create-donation"));

        await endpoint.Send<CreateDonation>(new { entity.CampaignId, entity.Value });

    }

    public void DeleteById(long id)
    {
        _logger.LogInformation($"Iniciando serviço 'DELETE' por Id: {id} de Campanha !");

        var entity = _repository.GetById(id);

        if (entity == null)
        {
            _logger.LogWarning($"Registro não encontrado para o id: {id}");
            throw new NotFoundException($"Registro não encontrado para o id: {id}");
        }

        _repository.DeleteById(id);

        _logger.LogInformation($"Campanha com id {id} removido com sucesso !");
    }

    public IEnumerable<CampaignOutputDto> GetActiveCampaigns()
    {
        _logger.LogInformation("Iniciando serviço 'GetActiveCampaigns' de Campanha !");

        return ParseModel.Map<ICollection<CampaignOutputDto>>(_repository.GetActiveCampaigns());
    }

    public ICollection<CampaignOutputDto> GetAll()
    {
        _logger.LogInformation("Iniciando serviço 'GETALL' de Campanha !");

        return ParseModel.Map<ICollection<CampaignOutputDto>>(_repository.GetAll());
    }

    public CampaignOutputDto? GetById(long id)
    {
        _logger.LogInformation($"Iniciando serviço 'GET' por Id: {id} de Campanha !");

        var result = _repository.GetById(id);

        if (result != null)
            return ParseModel.Map<CampaignOutputDto>(result);
        else
        {
            _logger.LogWarning($"Campanha com Id: {id} não encontrado !");
            throw new NotFoundException($"Registro não encontrado para o id: {id}"); ;
        }
    }

    public void Update(CampaignUpdateDto entity)
    {
        var errors = new Dictionary<string, string[]>();

        _logger.LogInformation($"Iniciando serviço 'UPDATE' de Campanha com Id {entity.Id}!");

        if (entity.StartedDateTime < DateTime.Now)
        {
            _logger.LogError("Data de criação inválida");
            errors["Email"] = ["Data de criação inválida"];
        }

        if (entity.FinancialGoal <= 0)
        {
            _logger.LogError("Meta financeira deve ser maior que 0");
            errors["Email"] = ["Meta financeira deve ser maior que 0"];
        }

        if (errors.Any())
            throw new BadRequestException("Erro de validação", errors);

        _repository.Update(new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            Description = entity.Description,
            StartedDateTime = entity.StartedDateTime,
            FinishedDateTime = entity.FinishedDateTime,
            FinancialGoal = entity.FinancialGoal,
            Status = entity.Status
        });

        _logger.LogInformation($"Campanha com Id {entity.Id} atualizado com sucesso !");
    }

    public CampaignOutputDto UpdateCampaignDoanatedValue(long campaignId, decimal value)
    {
        _logger.LogInformation("Atualizando valor arrecadado de Campanha !");

        return ParseModel.Map<CampaignOutputDto>(_repository.UpdateCampaignDoanatedValue(campaignId, value));
    }
}
