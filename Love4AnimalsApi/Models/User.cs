namespace Love4AnimalsApi.Models;

public enum RolEnum
{
    MISIONERO,
    DONADOR
}

public class User
{
    public User(int id, string nombre, string email, string passwordHash, RolEnum rol)
    {
        Id = id;
        Nombre = nombre;
        Email = email;
        PasswordHash = passwordHash;
        Rol = rol;
    }

    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public RolEnum Rol { get; set; }

    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<Donation> Donations { get; set; } = [];
}