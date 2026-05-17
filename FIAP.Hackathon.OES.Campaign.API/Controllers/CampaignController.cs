using FIAP.Hackathon.OES.Campaign.Service.Dto.User;
using FIAP.Hackathon.OES.Campaign.Service.Interfaces;
using FIAP.Hackathon.OES.User.Service.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.FCG.Campaign.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CampaignController(ICampaignService service) : ControllerBase
{
    private readonly ICampaignService _service = service;

    /// <summary>
    /// Cria uma nova campanha.
    /// </summary>
    /// <param name="input">Dados do campanha a ser criado.</param>
    /// <response code="200">Campanha criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [Authorize(Policy = "MANAGER")]
    public async Task<IActionResult> Post([FromBody] CampaignCreateDto input)
    {
        await _service.Create(input);
        return Ok();
    }

    /// <summary>
    /// Atualiza uma campanha existente.
    /// </summary>
    /// <param name="input">Dados do campanha para atualização.</param>
    /// <response code="200">Campanha atualizado com sucesso.</response>
    /// <response code="404">Campanha não encontrado.</response>
    [HttpPut]
    [Authorize(Policy = "MANAGER")]
    public async Task<IActionResult> Put([FromBody] CampaignUpdateDto input)
    {
        _service.Update(input);
        return Ok();
    }

    /// <summary>
    /// Remove uma campanha pelo ID.
    /// </summary>
    /// <param name="id">ID do campanha.</param>
    /// <response code="200">Campanha removido com sucesso.</response>
    /// <response code="404">Campanha não encontrado.</response>
    [HttpDelete("{id:long}")]
    [Authorize(Policy = "MANAGER")]
    public async Task<IActionResult> Delete(long id)
    {
        _service.DeleteById(id);
        return Ok();
    }

    /// <summary>
    /// Obtém uma campanha pelo ID.
    /// </summary>
    /// <param name="id">ID do campanha.</param>
    /// <response code="200">Campanha encontrado.</response>
    /// <response code="404">Campanha não encontrado.</response>
    [HttpGet("GetById/{id:long}")]
    [Authorize(Policy = "MANAGER")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(_service.GetById(id));
    }

    /// <summary>
    /// Lista todas campanhas.
    /// </summary>
    /// <response code="200">Lista de campanhas retornada com sucesso.</response>
    [HttpGet("GetAll")]
    [Authorize(Policy = "MANAGER")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_service.GetAll());
    }

    /// <summary>
    /// Lista todas campanhas ativas.
    /// </summary>
    /// <response code="200">Lista de campanhas retornada com sucesso.</response>
    [HttpGet("GetActiveCampaigns")]
    public async Task<IActionResult> GetActiveCampaigns()
    {
        return Ok(_service.GetActiveCampaigns());
    }

    /// <summary>
    /// Cria uma doação.
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("PostDonation")]
    public async Task<IActionResult> PostDonation([FromBody] CampaignCreateDonationDto input)
    {
        await _service.CreateDonationAsync(input);
        return Ok();
    }

    /// <summary>
    /// Atualiza o valor de uma Campanha.
    /// </summary>
    /// <response code="200"></response>
    /// <param name="request">id e valor.</param>
    /// <response code="200">Campanha atualizada com sucesso.</response>
    /// <response code="404">Campanha não encontrado.</response>
    [HttpPut("UpdateCampaignValue")]
    [Authorize(Policy = "WORKER")]
    public async Task<IActionResult> UpdateCampaignValue(CampaignUpdateValueDto request)
    {
        return Ok(_service.UpdateCampaignDoanatedValue(request.CampaignId, request.Value));
    }

}
