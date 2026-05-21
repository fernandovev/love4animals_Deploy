using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    [Tags("Autenticación")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        [EndpointSummary("Registrar usuario")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<AuthResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var response = await authService.RegisterAsync(dto);

            if (response == null)
                return Conflict(new { message = "El email ya está registrado" });

            return CreatedAtAction(
                nameof(Register),
                new { id = response.Id },
                response
            );
        }

        [HttpPost("login")]
        [EndpointSummary("Iniciar sesión")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<AuthResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var response = await authService.LoginAsync(dto);

            if (response == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(response);
        }
    }
}