using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class DeporteController : ControllerBase
    {
        private readonly DeporteNegocio DeporteNegocio;

        private readonly ILogger<Club> logger;

        public DeporteController(DeporteNegocio deporteNegocio, ILogger<Club> logger)
        {
            this.DeporteNegocio = deporteNegocio;
            this.logger = logger;
        }

        // GET: api/<ClubController>
        [HttpGet]
        public ActionResult<List<Deporte>> GetAll()
        {
            if (ModelState.IsValid)
            {
                List<Deporte> deportes = new List<Deporte>();
                try
                {
                    deportes = DeporteNegocio.GetAll().ToList();

                    if (deportes.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(deportes);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }


        // GET api/<DeporteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DeporteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DeporteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DeporteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
