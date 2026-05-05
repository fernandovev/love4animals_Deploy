namespace Love4AnimalsApi.Dtos;

public record GetCommentDto(
    int Id,
    int PostId,
    string Texto,
    DateTime Fecha
);