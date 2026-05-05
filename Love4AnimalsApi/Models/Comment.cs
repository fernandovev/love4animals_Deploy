namespace Love4AnimalsApi.Models;

public class Comment
{
    public Comment(int id, int postId, string texto, DateTime fecha)
    {
        Id = id;
        PostId = postId;
        Texto = texto;
        Fecha = fecha;
    }

    public int Id { get; set; }
    public int PostId { get; set; }
    public string Texto { get; set; }
    public DateTime Fecha { get; set; }
}