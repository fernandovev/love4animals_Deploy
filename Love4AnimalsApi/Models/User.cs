namespace Love4AnimalsApi.Models;

public enum RolEnum
{
    MISIONERO,
    DONADOR
}

public class User
{
    public User(int id, string nombre, string email, string password, RolEnum rol)
    {
        Id = id;
        Nombre = nombre;
        Email = email;
        Password = password;
        Rol = rol;
    }

    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RolEnum Rol { get; set; }

    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<Donation> Donations { get; set; } = [];
}