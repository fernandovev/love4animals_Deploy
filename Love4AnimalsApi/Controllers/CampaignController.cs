using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/campaigns")]
    [Tags("Campañas")]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        [HttpGet]
        [EndpointSummary("Obtener campañas")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetCampaignDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCampaigns()
        {
            var campaigns = await campaignService.GetCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtener campaña por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetCampaignDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var campaign = await campaignService.GetCampaignByIdAsync(id);

            if (campaign == null)
                return NotFound(new { message = "Campaña no encontrada" });

            return Ok(campaign);
        }

        [Authorize(Roles = "MISIONERO")]
        [HttpPost]
        [EndpointSummary("Crear campaña")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetCampaignDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nueva = await campaignService.CreateCampaignAsync(dto);

            if (nueva == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return CreatedAtAction(
                nameof(GetCampaignById),
                new { id = nueva.Id },
                nueva
            );
        }

        [HttpPut("{id}")]
        [EndpointSummary("Actualizar campaña")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetCampaignDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCampaign(int id, [FromBody] UpdateCampaignDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updatedCampaign = await campaignService.UpdateCampaignAsync(id, dto);

            if (updatedCampaign == null)
                return NotFound(new { message = "Campaña o usuario no encontrado" });

            return Ok(updatedCampaign);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar campaña")]
        [Produces("application/json")]
        [ProducesResponseType<GetCampaignDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var deletedCampaign = await campaignService.DeleteCampaignAsync(id);

            if (deletedCampaign == null)
                return NotFound(new { message = "Campaña no encontrada" });

            return Ok(deletedCampaign);
        }
    }
}