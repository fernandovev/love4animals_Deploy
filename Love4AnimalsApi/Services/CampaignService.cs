using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class CampaignService : ICampaignService
{
    private ICampaignRepository campaignRepository;

    public CampaignService(ICampaignRepository campaignRepository)
    {
        this.campaignRepository = campaignRepository;
    }

    public List<GetCampaignDto> GetCampaigns()
    {
        return campaignRepository.GetCampaigns()
            .Select(c => new GetCampaignDto(
                c.Id,
                c.Titulo,
                c.MetaRecaudacion,
                c.MontoActual,
                c.Estado,
                c.Descripcion
            ))
            .ToList();
    }

    public GetCampaignDto? GetCampaignById(int id)
    {
        var campaign = campaignRepository.GetCampaignById(id);

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

    public GetCampaignDto CreateCampaign(CreateCampaignDto dto)
    {
        var campaigns = campaignRepository.GetCampaigns();
        int newId = campaigns.Count > 0 ? campaigns.Max(c => c.Id) + 1 : 1;

        Campaign newCampaign = new Campaign(
            newId,
            dto.Titulo,
            dto.MetaRecaudacion,
            dto.MontoActual,
            dto.Estado,
            dto.Descripcion
        );

        var createdCampaign = campaignRepository.CreateCampaign(newCampaign);

        return new GetCampaignDto(
            createdCampaign.Id,
            createdCampaign.Titulo,
            createdCampaign.MetaRecaudacion,
            createdCampaign.MontoActual,
            createdCampaign.Estado,
            createdCampaign.Descripcion
        );
    }

    public GetCampaignDto? UpdateCampaign(int id, UpdateCampaignDto dto)
    {
        var campaign = campaignRepository.GetCampaignById(id);

        if (campaign == null)
            return null;

        campaign.Titulo = dto.Titulo;
        campaign.MetaRecaudacion = dto.MetaRecaudacion;
        campaign.MontoActual = dto.MontoActual;
        campaign.Estado = dto.Estado;
        campaign.Descripcion = dto.Descripcion;

        return new GetCampaignDto(
            campaign.Id,
            campaign.Titulo,
            campaign.MetaRecaudacion,
            campaign.MontoActual,
            campaign.Estado,
            campaign.Descripcion
        );
    }

    public GetCampaignDto? DeleteCampaign(int id)
    {
        var campaign = campaignRepository.GetCampaignById(id);

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

        campaignRepository.DeleteCampaign(id);

        return deletedCampaign;
    }
}