using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/users")]
    [Tags("Usuarios")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [EndpointSummary("Obtener usuarios")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetUserDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtener usuario por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetUserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(user);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Actualizar usuario")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetUserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updatedUser = await userService.UpdateUserAsync(id, dto);

            if (updatedUser == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar usuario")]
        [Produces("application/json")]
        [ProducesResponseType<GetUserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await userService.DeleteUserAsync(id);

            if (deletedUser == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(deletedUser);
        }
    }
}