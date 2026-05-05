using System.ComponentModel.DataAnnotations;

namespace Love4AnimalsApi.Dtos;

public record CreateCommentDto(
    [Required(ErrorMessage = "El texto del comentario es obligatorio")]
    [MaxLength(300, ErrorMessage = "El comentario no puede superar los 300 caracteres")]
    string Texto
);