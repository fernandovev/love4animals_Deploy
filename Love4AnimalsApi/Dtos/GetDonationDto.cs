using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record GetDonationDto(
    int Id,
    double Monto,
    DateTime Fecha,
    EstadoDonacionEnum Estado
);