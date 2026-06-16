using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces;

public interface ICampaignService
{
    Task<List<GetCampaignDto>> GetCampaignsAsync();
    Task<GetCampaignDto?> GetCampaignByIdAsync(int id);
    Task<GetCampaignDto?> CreateCampaignAsync(CreateCampaignDto dto);
    Task<GetCampaignDto?> UpdateCampaignAsync(int id, UpdateCampaignDto dto);
    Task<GetCampaignDto?> DeleteCampaignAsync(int id);
}