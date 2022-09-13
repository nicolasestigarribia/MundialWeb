using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class PersonaController : ControllerBase
	{

        private readonly PersonaNegocio PersonaNegocio;

        private readonly ILogger<PersonaNegocio> logger;

        public PersonaController(PersonaNegocio personaNegocio, ILogger<PersonaNegocio> logger)
        {
            this.PersonaNegocio = personaNegocio;
            this.logger = logger;
        }

        // GET: api/<PersonaController>
        [HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<PersonaController>/5
		[HttpGet("{id}")]
		public ActionResult<PerfilItem> GetById(int id)
		{
            if (ModelState.IsValid)
            {
                try
                {
                    var persona = PersonaNegocio.GetById(id);
                    if (persona == null)
                    {
                        return NotFound();
                    }
                    return Ok(persona);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }


        [HttpGet("Legajo/{legajo}")]
        public ActionResult<PerfilItem> GetByLegajo(int legajo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var persona = PersonaNegocio.GetByLegajo(legajo);
                    if (persona == null)
                    {
                        return NotFound();
                    }
                    return StatusCode(200, persona);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al insertar nuevo usuario a la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Cuit/{cuit}")]
        public ActionResult<PerfilItem> GetByCuit(string cuit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var persona = PersonaNegocio.GetByCuit(cuit);
                    if (persona == null)
                    {
                        return NotFound();
                    }
                    return StatusCode(200, persona);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al insertar nuevo usuario a la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        // POST api/<PersonaController>
        [HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<PersonaController>/5
		[HttpPut("{id}")]
		public ActionResult<Persona> Update(int id, [FromBody] Persona persona)
		{
            if (id != persona.IdPersona)
            {
                logger.LogWarning("Error al actualiar el modelo {modelo}. Error entre id de usuario y id por parámetro", "usuario");
                return BadRequest(ModelState);
            }
            try
            {
                 var rta = PersonaNegocio.Update(persona);
                if(rta)
                {
                    return StatusCode(200);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                return StatusCode(500, ModelState);
            }
            return BadRequest();
        }

		// DELETE api/<PersonaController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
