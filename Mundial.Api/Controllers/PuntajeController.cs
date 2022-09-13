using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class PuntajeController : ControllerBase
	{

        private readonly UsuarioRespuestaNegocio usuarioRespuestaNegocio;

        private readonly ILogger<PuntajeController> logger;

		public PuntajeController(UsuarioRespuestaNegocio usuarioRespuestaNegocio, ILogger<PuntajeController> logger)
		{
			this.usuarioRespuestaNegocio = usuarioRespuestaNegocio;
			this.logger = logger;
		}

        [HttpGet("Actualizar")]
        public ActionResult<int> ActualizarPuntajes()
		{
			if (ModelState.IsValid)
			{
				try
				{
					var cantRegistrosActualizados = usuarioRespuestaNegocio.ActualizaPuntajesObtenidos();

					if (cantRegistrosActualizados == 0)
					{
						return StatusCode(202);
					}

					return StatusCode(200, cantRegistrosActualizados);

				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error al querer actualizar los puntajes", ex.Message);
					return StatusCode(500, ModelState);
				}
			}
			return StatusCode(400);
		}

        [HttpGet("Ranking")]
        public ActionResult<List<RankingUsuarioItem>> GetListaRankingUsuarios()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rankingUsuarios = usuarioRespuestaNegocio.GetRankingUsuarios();
                    if (rankingUsuarios.Count() > 0)
                    {
                        return StatusCode(200, rankingUsuarios);
                    }
                    return NoContent();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al generar ranking de usuarios", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }


    }
}
