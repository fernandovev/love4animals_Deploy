using System.ComponentModel.DataAnnotations;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record UpdateUserDto(
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    string Nombre,

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    string Email,

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres")]
    [MaxLength(50, ErrorMessage = "La contraseña no puede superar los 50 caracteres")]
    string Password,

    [Required(ErrorMessage = "El rol es obligatorio")]
    RolEnum Rol
);