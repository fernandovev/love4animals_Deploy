using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository campaignRepository;

    public CampaignService(ICampaignRepository campaignRepository)
    {
        this.campaignRepository = campaignRepository;
    }

    public async Task<List<GetCampaignDto>> GetCampaignsAsync()
    {
        var campaigns = await campaignRepository.GetCampaignsAsync();

        return campaigns.Select(c => new GetCampaignDto(
            c.Id,
            c.Titulo,
            c.MetaRecaudacion,
            c.MontoActual,
            c.Estado,
            c.Descripcion
        )).ToList();
    }

    public async Task<GetCampaignDto?> GetCampaignByIdAsync(int id)
    {
        var campaign = await campaignRepository.GetCampaignByIdAsync(id);

        if (campaign == null)
            return null;

        return new GetCampaignDto(
            campaign.Id,
            campaign.Titulo,
            campaign.MetaRecaudacion,
            campaign.MontoActual,
            campaign.Estado,
            campaign.Descripcion
        );
    }

    public async Task<GetCampaignDto> CreateCampaignAsync(CreateCampaignDto dto)
    {
        Campaign newCampaign = new Campaign(
            0,
            dto.Titulo,
            dto.MetaRecaudacion,
            dto.MontoActual,
            dto.Estado,
            dto.Descripcion
        );

        var createdCampaign = await campaignRepository.CreateCampaignAsync(newCampaign);

        return new GetCampaignDto(
            createdCampaign.Id,
            createdCampaign.Titulo,
            createdCampaign.MetaRecaudacion,
            createdCampaign.MontoActual,
            createdCampaign.Estado,
            createdCampaign.Descripcion
        );
    }

    public async Task<GetCampaignDto?> UpdateCampaignAsync(int id, UpdateCampaignDto dto)
    {
        var campaign = await campaignRepository.GetCampaignByIdAsync(id);

        if (campaign == null)
            return null;

        campaign.Titulo = dto.Titulo;
        campaign.MetaRecaudacion = dto.MetaRecaudacion;
        campaign.MontoActual = dto.MontoActual;
        campaign.Estado = dto.Estado;
        campaign.Descripcion = dto.Descripcion;

        await campaignRepository.UpdateCampaignAsync(campaign);

        return new GetCampaignDto(
            campaign.Id,
            campaign.Titulo,
            campaign.MetaRecaudacion,
            campaign.MontoActual,
            campaign.Estado,
            campaign.Descripcion
        );
    }

    public async Task<GetCampaignDto?> DeleteCampaignAsync(int id)
    {
        var campaign = await campaignRepository.GetCampaignByIdAsync(id);

        if (campaign == null)
            return null;

        var deletedCampaign = new GetCampaignDto(
            campaign.Id,
            campaign.Titulo,
            campaign.MetaRecaudacion,
            campaign.MontoActual,
            campaign.Estado,
            campaign.Descripcion
        );

        await campaignRepository.DeleteCampaignAsync(campaign);

        return deletedCampaign;
    }
}