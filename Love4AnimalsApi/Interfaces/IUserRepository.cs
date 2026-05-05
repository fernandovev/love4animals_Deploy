using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}