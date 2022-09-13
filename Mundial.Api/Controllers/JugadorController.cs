using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;
namespace Mundial.Api.Controllers
{
    [Route("api/[controller]es")]
    [ApiController]
    public class JugadorController : ControllerBase
    {

        private readonly JugadorNegocio jugadorNegocio;

        private readonly ILogger<Jugador>  logger;

        public JugadorController(JugadorNegocio jugadorNegocio, ILogger<Jugador> logger)
        {
            this.jugadorNegocio = jugadorNegocio;
            this.logger = logger;
        }

        // GET: api/<Jugador>
        [HttpGet]
        public IActionResult GetAll()
        {
            if (ModelState.IsValid)
            {
               List<Jugador> jugadores = new List<Jugador>();
                try
                {
                    jugadores = jugadorNegocio.GetAll().ToList();

                    if (jugadores.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(jugadores);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        // GET api/<Jugador>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jugador = jugadorNegocio.GetById(id);
                    if (jugador == null)
                    {
                        return NotFound();
                    }
                    return Ok(jugador);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        //GET api/<JugadorController>/Equipo/id
        [HttpGet("Equipo/{id}")]
        public IActionResult GetListaJugadoresByIdEquipo(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listaJugadores = jugadorNegocio.GetListaJugadoresByIdEquipo(id);
                    if (listaJugadores.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(listaJugadores);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }



        // POST api/<JugadorController>
        [HttpPost]
        public ActionResult Post([FromBody] Jugador jugadorNuevo)
        {
            if(jugadorNuevo != null) 
            {
                try
                {
                    var rta = jugadorNegocio.Insert(jugadorNuevo);
                    if(rta == false)
                    {
                        return BadRequest();

                    }
                    return StatusCode(201, jugadorNuevo);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();

        }

        // PUT api/<Jugador>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromBody] Jugador jugadorUpdated)
        {
            if (id == jugadorUpdated.IdJugador)
            {
                try
                {
                    if (id > 0)
                    {
                        jugadorNegocio.Update(jugadorUpdated);
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

        // DELETE api/<Jugador>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if(id > 0)
                {
                    var jugador = jugadorNegocio.GetById(id);
                    if ( jugador == null)
                    {
                        return NotFound();
                    }
                    if (jugadorNegocio.Delete(jugador) == false)
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
    }
}
