using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/donations")]
    [Tags("Donaciones")]
    [Authorize]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService donationService;

        public DonationController(IDonationService donationService)
        {
            this.donationService = donationService;
        }

        [HttpGet]
        [EndpointSummary("Obtener donaciones")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetDonationDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDonations()
        {
            var donations = await donationService.GetDonationsAsync();
            return Ok(donations);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtener donación por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetDonationDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDonationById(int id)
        {
            var donation = await donationService.GetDonationByIdAsync(id);

            if (donation == null)
                return NotFound(new { message = "Donación no encontrada" });

            return Ok(donation);
        }

        [Authorize(Roles = "DONADOR")]
        [HttpPost]
        [EndpointSummary("Crear donación")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetDonationDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDonation([FromBody] CreateDonationDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nueva = await donationService.CreateDonationAsync(dto);

            if (nueva == null)
                return NotFound(new { message = "Usuario o campaña no encontrada" });

            return CreatedAtAction(
                nameof(GetDonationById),
                new { id = nueva.Id },
                nueva
            );
        }

        [HttpPut("{id}")]
        [EndpointSummary("Actualizar donación")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetDonationDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDonation(int id, [FromBody] UpdateDonationDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updatedDonation = await donationService.UpdateDonationAsync(id, dto);

            if (updatedDonation == null)
                return NotFound(new { message = "Donación no encontrada" });

            return Ok(updatedDonation);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar donación")]
        [Produces("application/json")]
        [ProducesResponseType<GetDonationDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            var deletedDonation = await donationService.DeleteDonationAsync(id);

            if (deletedDonation == null)
                return NotFound(new { message = "Donación no encontrada" });

            return Ok(deletedDonation);
        }
    }
}