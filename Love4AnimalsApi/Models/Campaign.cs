namespace Love4AnimalsApi.Models;

public enum EstadoCampaniaEnum
{
    ACTIVA,
    PAUSADA,
    FINALIZADA
}

public class Campaign
{
    public Campaign(int id, string titulo, double metaRecaudacion, double montoActual, EstadoCampaniaEnum estado, string descripcion)
    {
        Id = id;
        Titulo = titulo;
        MetaRecaudacion = metaRecaudacion;
        MontoActual = montoActual;
        Estado = estado;
        Descripcion = descripcion;
    }

    public int Id { get; set; }
    public string Titulo { get; set; }
    public double MetaRecaudacion { get; set; }
    public double MontoActual { get; set; }
    public EstadoCampaniaEnum Estado { get; set; }
    public string Descripcion { get; set; }

    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<Donation> Donations { get; set; } = [];
}