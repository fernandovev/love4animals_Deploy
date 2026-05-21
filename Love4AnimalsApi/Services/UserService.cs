using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;

namespace Love4AnimalsApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<List<GetUserDto>> GetUsersAsync()
    {
        var users = await userRepository.GetUsersAsync();

        return users.Select(user => new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Rol
        )).ToList();
    }

    public async Task<GetUserDto?> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if (user == null)
            return null;

        return new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Rol
        );
    }

    public async Task<GetUserDto?> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if (user == null)
            return null;

        user.Nombre = dto.Nombre;
        user.Email = dto.Email;
        user.Rol = dto.Rol;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12);
        }

        await userRepository.UpdateUserAsync(user);

        return new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Rol
        );
    }

    public async Task<GetUserDto?> DeleteUserAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if (user == null)
            return null;

        var deletedUser = new GetUserDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Rol
        );

        await userRepository.DeleteUserAsync(user);

        return deletedUser;
    }
}