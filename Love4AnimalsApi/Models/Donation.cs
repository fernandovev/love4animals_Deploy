namespace Love4AnimalsApi.Models;

public enum EstadoDonacionEnum
{
    PENDIENTE,
    CONFIRMADA,
    CANCELADA
}

public class Donation
{
    public Donation(int id, int userId, int campaignId, double monto, DateTime fecha, EstadoDonacionEnum estado)
    {
        Id = id;
        UserId = userId;
        CampaignId = campaignId;
        Monto = monto;
        Fecha = fecha;
        Estado = estado;
    }

    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; } = null!;

    public double Monto { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoDonacionEnum Estado { get; set; }
}