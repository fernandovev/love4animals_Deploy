using System.ComponentModel.DataAnnotations;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record UpdatePostDto(
    [Range(1, int.MaxValue, ErrorMessage = "El usuario es obligatorio")]
    int UserId,

    [Range(1, int.MaxValue, ErrorMessage = "La campaña es obligatoria")]
    int CampaignId,

    [Required(ErrorMessage = "El título es obligatorio")]
    [MaxLength(150, ErrorMessage = "El título no puede superar los 150 caracteres")]
    string Titulo,

    [Range(0.01, double.MaxValue, ErrorMessage = "La meta de recaudación debe ser mayor a 0")]
    double MetaRecaudacion,

    [Range(0, double.MaxValue, ErrorMessage = "El monto actual no puede ser negativo")]
    double MontoActual,

    [Required(ErrorMessage = "El estado es obligatorio")]
    EstadoCampaniaEnum Estado,

    [Required(ErrorMessage = "La imagen es obligatoria")]
    [MaxLength(255, ErrorMessage = "La ruta o nombre de imagen no puede superar los 255 caracteres")]
    string Imagen,

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [MaxLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
    string Descripcion
);