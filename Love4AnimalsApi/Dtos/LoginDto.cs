using System.ComponentModel.DataAnnotations;

namespace Love4AnimalsApi.Dtos;

public record LoginDto(
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    string Email,

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    string Password
);