using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Dtos;

public record GetPostDto(
    int Id,
    string Titulo,
    double MetaRecaudacion,
    double MontoActual,
    EstadoCampaniaEnum Estado,
    string Imagen,
    string Descripcion,
    DateTime FechaCreacion,
    int CantidadLikes,
    int CantidadComentarios,
    int CantidadCompartidos
);