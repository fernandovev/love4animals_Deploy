using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/users")]
    [Tags("Usuarios")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Obtiene la lista de usuarios registrados en el sistema.
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet]
        [EndpointSummary("Obtener usuarios")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetUserDto>>(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var users = userService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="id">Identificador único del usuario</param>
        /// <returns>Datos del usuario encontrado</returns>
        [HttpGet("{id}")]
        [EndpointSummary("Obtener usuario por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetUserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(int id)
        {
            var user = userService.GetUserById(id);

            if (user == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(user);
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="dto">Datos del usuario a crear</param>
        /// <returns>Usuario creado correctamente</returns>
        [HttpPost]
        [EndpointSummary("Crear usuario")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetUserDto>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nuevo = userService.CreateUser(dto);

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = nuevo.Id },
                nuevo
            );
        }

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="id">Identificador del usuario a actualizar</param>
        /// <param name="dto">Nuevos datos del usuario</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{id}")]
        [EndpointSummary("Actualizar usuario")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updated = userService.UpdateUser(id, dto);

            if (updated == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(updated);
        }

        /// <summary>
        /// Elimina un usuario del sistema.
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar usuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int id)
        {
            var deleted = userService.DeleteUser(id);

            if (deleted == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(deleted);
        }
    }
}