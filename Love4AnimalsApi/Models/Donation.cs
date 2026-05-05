namespace Love4AnimalsApi.Models;

public enum EstadoDonacionEnum
{
    PENDIENTE,
    CONFIRMADA,
    CANCELADA
}

public class Donation
{
    public Donation(int id, double monto, DateTime fecha, EstadoDonacionEnum estado)
    {
        Id = id;
        Monto = monto;
        Fecha = fecha;
        Estado = estado;
    }

    public int Id { get; set; }
    public double Monto { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoDonacionEnum Estado { get; set; }
}