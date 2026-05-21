using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;

    public AuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        var emailExists = await userRepository.EmailExistsAsync(dto.Email);

        if (emailExists)
            return null;

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12);

        User newUser = new User(
            0,
            dto.Nombre,
            dto.Email,
            passwordHash,
            dto.Rol
        );

        var createdUser = await userRepository.CreateUserAsync(newUser);

        return new AuthResponseDto(
            createdUser.Id,
            createdUser.Nombre,
            createdUser.Email,
            createdUser.Rol,
            "Usuario registrado correctamente"
        );
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await userRepository.GetUserByEmailAsync(dto.Email);

        if (user == null)
            return null;

        bool passwordOk = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!passwordOk)
            return null;

        return new AuthResponseDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Rol,
            "Login correcto"
        );
    }
}