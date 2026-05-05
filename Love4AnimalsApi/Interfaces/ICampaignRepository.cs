using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces;

public interface ICampaignRepository
{
    Task<List<Campaign>> GetCampaignsAsync();
    Task<Campaign?> GetCampaignByIdAsync(int id);
    Task<Campaign> CreateCampaignAsync(Campaign campaign);
    Task UpdateCampaignAsync(Campaign campaign);
    Task DeleteCampaignAsync(Campaign campaign);
}