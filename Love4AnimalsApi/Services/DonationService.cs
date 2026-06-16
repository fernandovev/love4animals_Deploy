using System.Text.Json;
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.Extensions.Caching.Distributed;


namespace Love4AnimalsApi.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository donationRepository;
    private readonly IUserRepository userRepository;
    private readonly ICampaignRepository campaignRepository;
    private readonly IDistributedCache cache;
    private const string DonationsCacheKey = "donations:list";

    public DonationService(
        IDonationRepository donationRepository,
        IUserRepository userRepository,
        ICampaignRepository campaignRepository,
        IDistributedCache cache)
    {
        this.donationRepository = donationRepository;
        this.userRepository = userRepository;
        this.campaignRepository = campaignRepository;
        this.cache = cache;
    }

    public async Task<List<GetDonationDto>> GetDonationsAsync()
    {
        var cached = await cache.GetStringAsync(DonationsCacheKey);

        if (cached != null)
            return JsonSerializer.Deserialize<List<GetDonationDto>>(cached)!;

        var donations = await donationRepository.GetDonationsAsync();

        var result = donations.Select(d => new GetDonationDto(
            d.Id,
            d.UserId,
            d.CampaignId,
            d.Monto,
            d.Fecha,
            d.Estado
        )).ToList();

        await cache.SetStringAsync(
            DonationsCacheKey,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

        return result;
    }

    public async Task<GetDonationDto?> GetDonationByIdAsync(int id)
    {
        var donation = await donationRepository.GetDonationByIdAsync(id);

        if (donation == null)
            return null;

        return new GetDonationDto(
            donation.Id,
            donation.UserId,
            donation.CampaignId,
            donation.Monto,
            donation.Fecha,
            donation.Estado
        );
    }

    public async Task<GetDonationDto?> CreateDonationAsync(CreateDonationDto dto)
    {
        var user = await userRepository.GetUserByIdAsync(dto.UserId);
        if (user == null)
            return null;

        var campaign = await campaignRepository.GetCampaignByIdAsync(dto.CampaignId);
        if (campaign == null)
            return null;

        Donation newDonation = new Donation(
            0,
            dto.UserId,
            dto.CampaignId,
            dto.Monto,
            DateTime.UtcNow,
            dto.Estado
        );

        var createdDonation = await donationRepository.CreateDonationAsync(newDonation);

        await cache.RemoveAsync(DonationsCacheKey);

        return new GetDonationDto(
            createdDonation.Id,
            createdDonation.UserId,
            createdDonation.CampaignId,
            createdDonation.Monto,
            createdDonation.Fecha,
            createdDonation.Estado
        );
    }

    public async Task<GetDonationDto?> UpdateDonationAsync(int id, UpdateDonationDto dto)
    {
        var donation = await donationRepository.GetDonationByIdAsync(id);

        if (donation == null)
            return null;

        var user = await userRepository.GetUserByIdAsync(dto.UserId);
        if (user == null)
            return null;

        var campaign = await campaignRepository.GetCampaignByIdAsync(dto.CampaignId);
        if (campaign == null)
            return null;

        donation.UserId = dto.UserId;
        donation.CampaignId = dto.CampaignId;
        donation.Monto = dto.Monto;
        donation.Fecha = DateTime.UtcNow;
        donation.Estado = dto.Estado;

        await donationRepository.UpdateDonationAsync(donation);

        await cache.RemoveAsync(DonationsCacheKey);

        return new GetDonationDto(
            donation.Id,
            donation.UserId,
            donation.CampaignId,
            donation.Monto,
            donation.Fecha,
            donation.Estado
        );
    }

    public async Task<GetDonationDto?> DeleteDonationAsync(int id)
    {
        var donation = await donationRepository.GetDonationByIdAsync(id);

        if (donation == null)
            return null;

        var deletedDonation = new GetDonationDto(
            donation.Id,
            donation.UserId,
            donation.CampaignId,
            donation.Monto,
            donation.Fecha,
            donation.Estado
        );

        await donationRepository.DeleteDonationAsync(donation);

        await cache.RemoveAsync(DonationsCacheKey);

        return deletedDonation;
    }
}