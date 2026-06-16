using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record GetCampaignDto(
    int Id,
    int UserId,
    string Titulo,
    double MetaRecaudacion,
    double MontoActual,
    EstadoCampaniaEnum Estado,
    string Descripcion
);