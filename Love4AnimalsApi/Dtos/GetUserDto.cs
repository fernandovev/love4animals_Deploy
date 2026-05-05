using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record GetUserDto(
    int Id,
    string Nombre,
    string Email,
    string Password,
    RolEnum Rol
);