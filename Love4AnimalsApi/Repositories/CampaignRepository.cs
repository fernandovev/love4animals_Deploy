using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly AppDbContext context;

    public CampaignRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Campaign>> GetCampaignsAsync()
    {
        return await context.Campaigns
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Campaign?> GetCampaignByIdAsync(int id)
    {
        return await context.Campaigns
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Campaign> CreateCampaignAsync(Campaign campaign)
    {
        context.Campaigns.Add(campaign);
        await context.SaveChangesAsync();
        return campaign;
    }

    public async Task UpdateCampaignAsync(Campaign campaign)
    {
        context.Campaigns.Update(campaign);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCampaignAsync(Campaign campaign)
    {
        context.Campaigns.Remove(campaign);
        await context.SaveChangesAsync();
    }
}