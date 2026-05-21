namespace Love4AnimalsApi.Models;

public class Post
{
    public Post(
        int id,
        int userId,
        int campaignId,
        string titulo,
        double metaRecaudacion,
        double montoActual,
        EstadoCampaniaEnum estado,
        string imagen,
        string descripcion,
        DateTime fechaCreacion,
        int cantidadLikes,
        int cantidadComentarios,
        int cantidadCompartidos)
    {
        Id = id;
        UserId = userId;
        CampaignId = campaignId;
        Titulo = titulo;
        MetaRecaudacion = metaRecaudacion;
        MontoActual = montoActual;
        Estado = estado;
        Imagen = imagen;
        Descripcion = descripcion;
        FechaCreacion = fechaCreacion;
        CantidadLikes = cantidadLikes;
        CantidadComentarios = cantidadComentarios;
        CantidadCompartidos = cantidadCompartidos;
    }

    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; } = null!;

    public string Titulo { get; set; }
    public double MetaRecaudacion { get; set; }
    public double MontoActual { get; set; }
    public EstadoCampaniaEnum Estado { get; set; }
    public string Imagen { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int CantidadLikes { get; set; }
    public int CantidadComentarios { get; set; }
    public int CantidadCompartidos { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];
}