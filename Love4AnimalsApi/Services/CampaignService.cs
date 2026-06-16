using System.Text.Json;
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Love4AnimalsApi.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository campaignRepository;
    private readonly IUserRepository userRepository;
    private readonly IDistributedCache cache;
    private const string CampaignsCacheKey = "campaigns:list";

    public CampaignService(
        ICampaignRepository campaignRepository,
        IUserRepository userRepository,
        IDistributedCache cache)
    {
        this.campaignRepository = campaignRepository;
        this.userRepository = userRepository;
        this.cache = cache;
    }

    public async Task<List<GetCampaignDto>> GetCampaignsAsync()
    {
        var cached = await cache.GetStringAsync(CampaignsCacheKey);

        if (cached != null)
            return JsonSerializer.Deserialize<List<GetCampaignDto>>(cached)!;

        var campaigns = await campaignRepository.GetCampaignsAsync();

        var result = campaigns.Select(c => new GetCampaignDto(
            c.Id,
            c.UserId,
            c.Titulo,
            c.MetaRecaudacion,
            c.MontoActual,
            c.Estado,
            c.Descripcion
        )).ToList();

        await cache.SetStringAsync(
            CampaignsCacheKey,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

        return result;
    }

    public async Task<GetCampaignDto?> GetCampaignByIdAsync(int id)
    {
        var campaign = await campaignRepository.GetCampaignByIdAsync(id);

        if (campaign == null)
            return null;

        return new GetCampaignDto(
            campaign.Id,
            campaign.UserId,
            campaign.Titulo,
            campaign.MetaRecaudacion,
            campaign.MontoActual,
            campaign.Estado,
            campaign.Descripcion
        );
    }

    public async Task<GetCampaignDto?> CreateCampaignAsync(CreateCampaignDto dto)
    {
        var user = await userRepository.GetUserByIdAsync(dto.UserId);

        if (user == null)
            return null;

        Campaign newCampaign = new Campaign(
            0,
            dto.UserId,
            dto.Titulo,
            dto.MetaRecaudacion,
            dto.MontoActual,
            dto.Estado,
            dto.Descripcion
        );

        var createdCampaign = await campaignRepository.CreateCampaignAsync(newCampaign);

        await cache.RemoveAsync(CampaignsCacheKey);

        return new GetCampaignDto(
            createdCampaign.Id,
            createdCampaign.UserId,
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

        var user = await userRepository.GetUserByIdAsync(dto.UserId);

        if (user == null)
            return null;

        campaign.UserId = dto.UserId;
        campaign.Titulo = dto.Titulo;
        campaign.MetaRecaudacion = dto.MetaRecaudacion;
        campaign.MontoActual = dto.MontoActual;
        campaign.Estado = dto.Estado;
        campaign.Descripcion = dto.Descripcion;

        await campaignRepository.UpdateCampaignAsync(campaign);

        await cache.RemoveAsync(CampaignsCacheKey);

        return new GetCampaignDto(
            campaign.Id,
            campaign.UserId,
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
            campaign.UserId,
            campaign.Titulo,
            campaign.MetaRecaudacion,
            campaign.MontoActual,
            campaign.Estado,
            campaign.Descripcion
        );

        await campaignRepository.DeleteCampaignAsync(campaign);

        await cache.RemoveAsync(CampaignsCacheKey);

        return deletedCampaign;
    }
}