using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/posts")]
    [Tags("Publicaciones")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        /// <summary>
        /// Obtiene la lista de publicaciones registradas en el sistema.
        /// </summary>
        [HttpGet]
        [EndpointSummary("Obtener publicaciones")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetPostDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await postService.GetPostsAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Obtiene una publicación por su identificador.
        /// </summary>
        [HttpGet("{id}")]
        [EndpointSummary("Obtener publicación por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetPostDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await postService.GetPostByIdAsync(id);

            if (post == null)
                return NotFound(new { message = "Publicación no encontrada" });

            return Ok(post);
        }

        /// <summary>
        /// Registra una nueva publicación en el sistema.
        /// </summary>
        [HttpPost]
[EndpointSummary("Crear publicación")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType<GetPostDto>(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
{
    if (!ModelState.IsValid)
        return ValidationProblem(ModelState);

    var nueva = await postService.CreatePostAsync(dto);

    if (nueva == null)
        return NotFound(new { message = "Usuario o campaña no encontrada" });

    return CreatedAtAction(
        nameof(GetPostById),
        new { id = nueva.Id },
        nueva
    );
}

        /// <summary>
        /// Actualiza la información de una publicación existente.
        /// </summary>
        [HttpPut("{id}")]
        [EndpointSummary("Actualizar publicación")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetPostDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updatedPost = await postService.UpdatePostAsync(id, dto);

            if (updatedPost == null)
                return NotFound(new { message = "Publicación no encontrada" });

            return Ok(updatedPost);
        }

        /// <summary>
        /// Elimina una publicación del sistema.
        /// </summary>
        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar publicación")]
        [Produces("application/json")]
        [ProducesResponseType<GetPostDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deletedPost = await postService.DeletePostAsync(id);

            if (deletedPost == null)
                return NotFound(new { message = "Publicación no encontrada" });

            return Ok(deletedPost);
        }
    }
}