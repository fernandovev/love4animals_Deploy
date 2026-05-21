using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces;

public interface IDonationRepository
{
    Task<List<Donation>> GetDonationsAsync();
    Task<Donation?> GetDonationByIdAsync(int id);
    Task<Donation> CreateDonationAsync(Donation donation);
    Task UpdateDonationAsync(Donation donation);
    Task DeleteDonationAsync(Donation donation);
}