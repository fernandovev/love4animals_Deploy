using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private List<Campaign> Campaigns { get; set; }

    public CampaignRepository()
    {
        Campaigns = new List<Campaign>();

        Campaigns.Add(new Campaign(
            1,
            "Rescate animal",
            5000,
            1000,
            EstadoCampaniaEnum.ACTIVA,
            "Campaña para rescatar animales"
        ));

        Campaigns.Add(new Campaign(
            2,
            "Alimento refugio",
            3000,
            500,
            EstadoCampaniaEnum.ACTIVA,
            "Recaudación para alimento"
        ));
    }

    public List<Campaign> GetCampaigns()
    {
        return Campaigns;
    }

    public Campaign? GetCampaignById(int id)
    {
        return Campaigns.FirstOrDefault(c => c.Id == id);
    }

    public Campaign CreateCampaign(Campaign campaign)
    {
        Campaigns.Add(campaign);
        return campaign;
    }

    public bool UpdateCampaign(int id, Campaign campaign)
    {
        var existing = Campaigns.FirstOrDefault(c => c.Id == id);

        if (existing == null)
            return false;

        existing.Titulo = campaign.Titulo;
        existing.MetaRecaudacion = campaign.MetaRecaudacion;
        existing.MontoActual = campaign.MontoActual;
        existing.Estado = campaign.Estado;
        existing.Descripcion = campaign.Descripcion;

        return true;
    }

    public bool DeleteCampaign(int id)
    {
        var campaign = Campaigns.FirstOrDefault(c => c.Id == id);

        if (campaign == null)
            return false;

        Campaigns.Remove(campaign);
        return true;
    }
}