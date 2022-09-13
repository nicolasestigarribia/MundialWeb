using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class RespuestaSimpleController : ControllerBase
    {
        private readonly RespuestaSimpleNegocio rtaSimpleNegocio;
        private readonly ILogger<RespuestaSimpleController> logger;

        List<int> ListaPreg22 = new List<int>() {126 ,127, 128, 129, 130, 131, 132, 133 ,134, 135, 136, 137, 26 ,27, 28, 29,138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153};

        public RespuestaSimpleController(RespuestaSimpleNegocio rtaSimpleNegocio, ILogger<RespuestaSimpleController> logger)
        {
            this.rtaSimpleNegocio = rtaSimpleNegocio;
            this.logger = logger;
        }

        // GET: api/<RespuestaSimpleController>
        [HttpGet]
        public ActionResult<IEnumerable<RespuestaSimple>> GetAll()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listaRespuesta = rtaSimpleNegocio.GetAll();
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

        [HttpGet("{idUser}")]
        public ActionResult<List<RespuestaSimple>> GetAllByIdUser(int idUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listaRespuesta = rtaSimpleNegocio.GetListaUsuarioRespuestasSimplesByIdUsuario(idUser);
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

        [HttpGet("{idUser}/Orden/{idOrden}")]
        public ActionResult<List<RespuestaSimple>> GetListaRespuestaSimplesByOrden(int idUser, int idOrden)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listaRespuesta = rtaSimpleNegocio.GetListaRespuestaByOrden(idUser,idOrden);
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

        // GET api/<RespuestaSimpleController>/5/6
        [HttpGet("{idPreg}/{idUser}")]
        public ActionResult<RespuestaSimple> GetById(int idPreg, int idUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var respuestaSimple = rtaSimpleNegocio.GetById(idPreg, idUser);
                    if (respuestaSimple == null)
                    {
                        return NotFound();
                    }
                    return Ok(respuestaSimple);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        // POST api/<RespuestaSimpleController>
        [HttpPost]
        public ActionResult<bool> Insert([FromBody] RespuestaSimple respuesta)
        {
            if (respuesta != null)
            {
                try
                {
                    var rta = rtaSimpleNegocio.Insert(respuesta);
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

        // PUT api/<RespuestaSimpleController>/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] RespuestaSimple rta)
        {
            if (id == rta.IdPregunta && rta.IdUsuario == rta.IdUsuario)
            {
                try
                {
                    if (id > 0)
                    {
                        rtaSimpleNegocio.Update(rta);
                        return NoContent();
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var rta = rtaSimpleNegocio.GetById(id);
                    if (rta == null)
                    {
                        return NotFound();
                    }
                    if (rtaSimpleNegocio.Delete(rta) == false)
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


        // DELETE api/<RespuestaSimpleController>/5
        [HttpDelete("Usuarios/{id}/{excepcion}")]
        public ActionResult DeleteAllByIdUser(int id, bool excepcion)
        {
            //con el parametro excepcion indico si estoy queriendo eliminar las respuestas de la pregunta 26, por lo tanto si es false, elimino las demas respuestas excepto la 26
            List<UsuariosRespuesta> listaRtasAEliminar = new List<UsuariosRespuesta>();
            try
            {
                if (id > 0)
                {
                    var listaRespuestas= rtaSimpleNegocio.GetListaUsuarioRespuestaByIdUsuario(id);
                    if (listaRespuestas.Count > 0)
                    {
                        foreach (var respuesta in listaRespuestas)
                        {
                            if(excepcion)
                            {
                                if (ListaPreg22.Contains(respuesta.IdPregunta) )
                                {
                                    listaRtasAEliminar.Add(respuesta);
                                }
                            }else
                            {
                                if (!ListaPreg22.Contains(respuesta.IdPregunta) && respuesta.IdPregunta != 22)
                                {
                                    listaRtasAEliminar.Add(respuesta);
                                }
                            }
                        }
                        rtaSimpleNegocio.DeleteMasivo(listaRtasAEliminar);
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


        [HttpPost("InsertMasivo")]
        public ActionResult<int> InsertMasivo([FromBody] List<RespuestaSimple> respuestaSimple)
        {
            int rta = 0;
            if (respuestaSimple.Count() > 0)
            {
                try
                {
                    rta = rtaSimpleNegocio.InsertMasivo(respuestaSimple);
                    return rta > 0 ? StatusCode(200, rta) : StatusCode(400, rta);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al insertar respuestas simples a la BDD", ex.Message);
                    return StatusCode(500, rta);
                }
            }
            else
            {
                return StatusCode(204);
            }
        }
    }
}
