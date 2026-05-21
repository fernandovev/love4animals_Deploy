using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly AppDbContext context;

    public DonationRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Donation>> GetDonationsAsync()
    {
        return await context.Donations
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Donation?> GetDonationByIdAsync(int id)
    {
        return await context.Donations
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Donation> CreateDonationAsync(Donation donation)
    {
        context.Donations.Add(donation);
        await context.SaveChangesAsync();
        return donation;
    }

    public async Task UpdateDonationAsync(Donation donation)
    {
        context.Donations.Update(donation);
        await context.SaveChangesAsync();
    }

    public async Task DeleteDonationAsync(Donation donation)
    {
        context.Donations.Remove(donation);
        await context.SaveChangesAsync();
    }
}