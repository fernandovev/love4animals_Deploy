using Love4AnimalsApi.Dtos;

namespace Love4AnimalsApi.Interfaces;

public interface IDonationService
{
    Task<List<GetDonationDto>> GetDonationsAsync();
    Task<GetDonationDto?> GetDonationByIdAsync(int id);
    Task<GetDonationDto?> CreateDonationAsync(CreateDonationDto dto);
    Task<GetDonationDto?> UpdateDonationAsync(int id, UpdateDonationDto dto);
    Task<GetDonationDto?> DeleteDonationAsync(int id);
}