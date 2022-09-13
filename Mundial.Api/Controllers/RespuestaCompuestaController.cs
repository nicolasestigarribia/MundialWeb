using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class RespuestaCompuestaController : ControllerBase
	{
		private readonly RespuestaCompuestaNegocio respuestaCompuestaNegocio;

		private readonly ILogger<RespuestaCompuestaController> logger;

		public RespuestaCompuestaController(RespuestaCompuestaNegocio respuestaCompuestaNegocio, ILogger<RespuestaCompuestaController> logger)
		{
			this.respuestaCompuestaNegocio = respuestaCompuestaNegocio;
			this.logger = logger;
		}

		// GET: api/<RespuestasCompuestasController>
		[HttpGet("{idUsuario}/{idPregunta}")]
		public ActionResult<IEnumerable<RespuestaCompuestaItem>> GetAll(int idUsuario, int idPregunta)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var listaRespuesta = new List<RespuestaCompuestaItem>();
					listaRespuesta = new RespuestaCompuestaNegocio().GetAllRtaCompuestaItemById(idUsuario, idPregunta);

					if (listaRespuesta.Count <= 0)
					{
						return NoContent();
					}
					return Ok(listaRespuesta);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
					return StatusCode(500, ModelState);
				}
			}
			return BadRequest();
		}

		[HttpGet("Usuarios/{idUsuario}")]
		public ActionResult<List<RespuestaCompuestaItem>> GetAllByIdUser(int idUsuario)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var listaRespuesta = new List<RespuestaCompuestaItem>();
					listaRespuesta = new RespuestaCompuestaNegocio().GetAllRtaCompuestaItemByUsuario(idUsuario);

					if (listaRespuesta.Count <= 0)
					{
						return NoContent();
					}
					return Ok(listaRespuesta);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
					return StatusCode(500, ModelState);
				}
			}
			return BadRequest();
		}

		// POST api/<RespuestaCompuestasController>
		[HttpPost]
		public ActionResult<bool> Insert([FromBody] RespuestaCompuestaItem respuestaCompuestas)
		{
			if (respuestaCompuestas != null)
			{
				try
				{
					bool rta = false;
					if(!new UsuarioRespuestaNegocio().ExisteRespuesta(respuestaCompuestas.IdUsuario, respuestaCompuestas.IdPregunta))
                    {
						rta = new UsuarioRespuestaNegocio().Insert(respuestaCompuestas);
					}
					rta = respuestaCompuestaNegocio.InsertItem(respuestaCompuestas);
					if (rta == false)
					{
						return BadRequest();
					}
					return StatusCode(201, rta);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
					return StatusCode(500, ModelState);
				}
			}
			return BadRequest();
		}

        [HttpPost("InsertMasivo")]
        public ActionResult<int> InsertMasivo([FromBody] List<RespuestaCompuestaItem> respuestaCompuestas)
		{
			int rta = 0;
			if(respuestaCompuestas.Count() > 0)
			{
				try
				{
                    foreach (var rtaCompuesta in respuestaCompuestas)
                    {
                        if (!new UsuarioRespuestaNegocio().ExisteRespuesta(rtaCompuesta.IdUsuario, rtaCompuesta.IdPregunta))
                        {
                            new UsuarioRespuestaNegocio().Insert(rtaCompuesta);
                        }
                    }
                    rta = respuestaCompuestaNegocio.InsertMasivo(respuestaCompuestas);
					return rta > 0 ? StatusCode(200,rta) : StatusCode(400, rta);
                }
				catch (Exception ex)
				{
                    logger.LogError(ex, "Error al insertar respuestas compuestas a la BDD", ex.Message);
					return StatusCode(500, rta);
                }
            }else
			{
				return StatusCode(204);
			}
		}

            // PUT api/<PreguntaCompuestaController>/5
        [HttpPut("{id}")]
		public ActionResult Update(int id, [FromBody] RespuestaCompuestaItem rta)
		{
			if (id == rta.IdPregunta)
			{
				try
				{
					if (id > 0)
					{
						var response = respuestaCompuestaNegocio.Updateitem(rta);
						return StatusCode(204 , response);
					}
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
					return StatusCode(500, ModelState);
				}
			}
			return BadRequest();
		}
	

		// DELETE api/<PreguntaCompuestaController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			try
			{
				if (id > 0)
				{
					var rta = respuestaCompuestaNegocio.GetById(id);
					if (rta == null)
					{
						return NotFound();
					}
					if (respuestaCompuestaNegocio.Delete(rta) == false)
					{
						return BadRequest();
					}
					return NoContent();
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
				return StatusCode(500, ModelState);
			}
			return BadRequest();
		}

		[HttpDelete("Usuarios/{id}")]
		public ActionResult DeleteAll(int id)
		{
            List<RespuestasCompuesta> listaRtasAEliminar = new List<RespuestasCompuesta>();
            try
            {
				if (id > 0)
				{
					var listaRtaCompuesta = respuestaCompuestaNegocio.GetAllByIdUsuario(id);
                    if (listaRtaCompuesta.Count > 0)
                    {
                        foreach (var rtaComp in listaRtaCompuesta)
                        {
							if(rtaComp.IdPregunta != 22)
                            {
                                listaRtasAEliminar.Add(rtaComp);
                            }
						}
						respuestaCompuestaNegocio.DeleteMasivo(listaRtasAEliminar);
                    }
                    return NoContent();
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
				return StatusCode(500, ModelState);
			}
			return BadRequest();
		}

		[HttpDelete("Pregunta/{id}/{iduser}")]
		public ActionResult DeleteByIdPregunta(int id, int idUser)
		{
            List<RespuestasCompuesta> listaRtasAEliminar = new List<RespuestasCompuesta>();
            try
            {
				if (id > 0)
				{
					var listaRtaCompuesta = respuestaCompuestaNegocio.GetAllByIdPreguntaAndIdUser(id, idUser);
					if (listaRtaCompuesta.Count > 0)
					{
						foreach (var rtaComp in listaRtaCompuesta)
						{
							listaRtasAEliminar.Add(rtaComp);
						}
						respuestaCompuestaNegocio.DeleteMasivo(listaRtasAEliminar);
					}
					return NoContent();
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
				return StatusCode(500, ModelState);
			}
			return BadRequest();
		}

	}
}
