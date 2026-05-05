using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/posts/{postId}/comments")]
    [Tags("Comentarios")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        /// <summary>
        /// Obtiene la lista de comentarios asociados a una publicación.
        /// </summary>
        [HttpGet]
        [EndpointSummary("Obtener comentarios por publicación")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetCommentDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var comments = await commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        /// <summary>
        /// Obtiene un comentario por su identificador dentro de una publicación.
        /// </summary>
        [HttpGet("{id}")]
        [EndpointSummary("Obtener comentario por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetCommentDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentById(int postId, int id)
        {
            var comment = await commentService.GetCommentByIdAsync(postId, id);

            if (comment == null)
                return NotFound(new { message = "Comentario no encontrado" });

            return Ok(comment);
        }

        /// <summary>
        /// Registra un nuevo comentario asociado a una publicación.
        /// </summary>
        [HttpPost]
        [EndpointSummary("Crear comentario")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetCommentDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nuevo = await commentService.CreateCommentAsync(postId, dto);

            if (nuevo == null)
                return NotFound(new { message = "Post no encontrado" });

            return CreatedAtAction(
                nameof(GetCommentById),
                new { postId = postId, id = nuevo.Id },
                nuevo
            );
        }

        /// <summary>
        /// Actualiza la información de un comentario existente.
        /// </summary>
        [HttpPut("{id}")]
        [EndpointSummary("Actualizar comentario")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetCommentDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateComment(int postId, int id, [FromBody] UpdateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updatedComment = await commentService.UpdateCommentAsync(postId, id, dto);

            if (updatedComment == null)
                return NotFound(new { message = "Comentario no encontrado" });

            return Ok(updatedComment);
        }

        /// <summary>
        /// Elimina un comentario asociado a una publicación.
        /// </summary>
        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar comentario")]
        [Produces("application/json")]
        [ProducesResponseType<GetCommentDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComment(int postId, int id)
        {
            var deletedComment = await commentService.DeleteCommentAsync(postId, id);

            if (deletedComment == null)
                return NotFound(new { message = "Comentario no encontrado" });

            return Ok(deletedComment);
        }
    }
}