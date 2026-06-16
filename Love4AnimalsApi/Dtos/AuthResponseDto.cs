using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record AuthResponseDto(
    int Id,
    string Nombre,
    string Email,
    RolEnum Rol,
    string Token,
    string RefreshToken,
    string Message
);