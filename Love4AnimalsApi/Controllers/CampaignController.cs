using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Love4AnimalsApi.Controllers
{
    [ApiController]
    [Route("v1/campaigns")]
    [Tags("Campañas")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        /// <summary>
        /// Obtiene la lista de campañas registradas en el sistema.
        /// </summary>
        /// <returns>Lista de campañas</returns>
        [HttpGet]
        [EndpointSummary("Obtener campañas")]
        [Produces("application/json")]
        [ProducesResponseType<List<GetCampaignDto>>(StatusCodes.Status200OK)]
        public IActionResult GetCampaigns()
        {
            var campaigns = campaignService.GetCampaigns();
            return Ok(campaigns);
        }

        /// <summary>
        /// Obtiene una campaña por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la campaña</param>
        /// <returns>Datos de la campaña encontrada</returns>
        [HttpGet("{id}")]
        [EndpointSummary("Obtener campaña por ID")]
        [Produces("application/json")]
        [ProducesResponseType<GetCampaignDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCampaignById(int id)
        {
            var campaign = campaignService.GetCampaignById(id);

            if (campaign == null)
                return NotFound(new { message = "Campaña no encontrada" });

            return Ok(campaign);
        }

        /// <summary>
        /// Registra una nueva campaña en el sistema.
        /// </summary>
        /// <param name="dto">Datos de la campaña a crear</param>
        /// <returns>Campaña creada correctamente</returns>
        [HttpPost]
        [EndpointSummary("Crear campaña")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType<GetCampaignDto>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCampaign([FromBody] CreateCampaignDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nueva = campaignService.CreateCampaign(dto);

            return CreatedAtAction(
                nameof(GetCampaignById),
                new { id = nueva.Id },
                nueva
            );
        }

        /// <summary>
        /// Actualiza la información de una campaña existente.
        /// </summary>
        /// <param name="id">Identificador de la campaña a actualizar</param>
        /// <param name="dto">Nuevos datos de la campaña</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{id}")]
        [EndpointSummary("Actualizar campaña")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCampaign(int id, [FromBody] UpdateCampaignDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updated = campaignService.UpdateCampaign(id, dto);

            if (updated == null)
                return NotFound(new { message = "Campaña no encontrada" });

            return Ok(updated);
        }

        /// <summary>
        /// Elimina una campaña del sistema.
        /// </summary>
        /// <param name="id">Identificador de la campaña a eliminar</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        [EndpointSummary("Eliminar campaña")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCampaign(int id)
        {
            var deleted = campaignService.DeleteCampaign(id);

            if (deleted == null)
                return NotFound(new { message = "Campaña no encontrada" });

            return Ok(deleted);
        }
    }
}