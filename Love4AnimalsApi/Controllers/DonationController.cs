using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/donations")]
    [Tags("Donaciones")]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService donationService;

        public DonationController(IDonationService donationService)
        {
            this.donationService = donationService;
        }

        /// <summary>
        /// Obtiene la lista de donaciones registradas en el sistema.
        /// </summary>
        /// <returns>Lista de donaciones</returns>
        [HttpGet]
        [EndpointSummary("Obtener donaciones")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetDonationDto>>(StatusCodes.Status200OK)]
        public IActionResult GetDonations()
        {
            var donations = donationService.GetDonations();
            return Ok(donations);
        }

        /// <summary>
        /// Obtiene una donación por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la donación</param>
        /// <returns>Datos de la donación encontrada</returns>
        [HttpGet("{id}")]
        [EndpointSummary("Obtener donación por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetDonationDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDonationById(int id)
        {
            var donation = donationService.GetDonationById(id);

            if (donation == null)
                return NotFound(new { message = "Donación no encontrada" });

            return Ok(donation);
        }

        /// <summary>
        /// Registra una nueva donación en el sistema.
        /// </summary>
        /// <param name="dto">Datos de la donación a crear</param>
        /// <returns>Donación creada correctamente</returns>
        [HttpPost]
        [EndpointSummary("Crear donación")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetDonationDto>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public IActionResult CreateDonation([FromBody] CreateDonationDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nueva = donationService.CreateDonation(dto);

            return CreatedAtAction(
                nameof(GetDonationById),
                new { id = nueva.Id },
                nueva
            );
        }

        /// <summary>
        /// Actualiza la información de una donación existente.
        /// </summary>
        /// <param name="id">Identificador de la donación a actualizar</param>
        /// <param name="dto">Nuevos datos de la donación</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{id}")]
        [EndpointSummary("Actualizar donación")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDonation(int id, [FromBody] UpdateDonationDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updated = donationService.UpdateDonation(id, dto);

            if (updated == null)
                return NotFound(new { message = "Donación no encontrada" });

            return Ok(updated);
        }

        /// <summary>
        /// Elimina una donación del sistema.
        /// </summary>
        /// <param name="id">Identificador de la donación a eliminar</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar donación")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDonation(int id)
        {
            var deleted = donationService.DeleteDonation(id);

            if (deleted == null)
                return NotFound(new { message = "Donación no encontrada" });

            return Ok(deleted);
        }
    }
}