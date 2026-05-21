using System.ComponentModel.DataAnnotations;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record UpdateDonationDto(
    [Range(1, int.MaxValue, ErrorMessage = "El usuario es obligatorio")]
    int UserId,

    [Range(1, int.MaxValue, ErrorMessage = "La campaña es obligatoria")]
    int CampaignId,

    [Range(0.01, double.MaxValue, ErrorMessage = "El monto de la donación debe ser mayor a 0")]
    double Monto,

    [Required(ErrorMessage = "El estado de la donación es obligatorio")]
    EstadoDonacionEnum Estado
);