using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace Love4AnimalsApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
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

        string token = GenerateJwtToken(createdUser);
        string refreshToken = GenerateRefreshToken();

        return new AuthResponseDto(
            createdUser.Id,
            createdUser.Nombre,
            createdUser.Email,
            createdUser.Rol,
            token,
            refreshToken,
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

        string token = GenerateJwtToken(user);
        string refreshToken = GenerateRefreshToken();

        return new AuthResponseDto(
            user.Id,
            user.Nombre,
            user.Email,
            user.Rol,
            token,
            refreshToken,
            "Login correcto"
        );
    }

    private string GenerateJwtToken(User user)
    {
        var jwt = configuration.GetSection("Jwt");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Rol.ToString())
        };

        var expiresMinutes = int.Parse(jwt["ExpiresMinutes"]!);

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
    }
}