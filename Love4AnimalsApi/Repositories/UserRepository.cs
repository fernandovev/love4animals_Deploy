using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> Users { get; set; }

    public UserRepository()
    {
        Users = new List<User>();

        Users.Add(new User(1, "Ana", "ana@gmail.com", "1234", RolEnum.MISIONERO));
        Users.Add(new User(2, "Luis", "luis@gmail.com", "abcd", RolEnum.DONANTE));
    }

    public List<User> GetUsers()
    {
        return Users;
    }

    public User? GetUserById(int id)
    {
        return Users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User user)
    {
        Users.Add(user);
        return user;
    }

    public bool UpdateUser(int id, User user)
    {
        var existingUser = Users.FirstOrDefault(u => u.Id == id);

        if (existingUser == null)
            return false;

        existingUser.Nombre = user.Nombre;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.Rol = user.Rol;

        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return false;

        Users.Remove(user);
        return true;
    }
}