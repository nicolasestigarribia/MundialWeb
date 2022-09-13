using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {

        private readonly PreguntaNegocio preguntaNegocio;
        private readonly ILogger<PreguntaController> logger;

        public PreguntaController(PreguntaNegocio preguntaNegocio, ILogger<PreguntaController> logger)
        {
            this.preguntaNegocio = preguntaNegocio;
            this.logger = logger;
        }
        // GET: api/<PreguntaController>
        [HttpGet]
        public ActionResult<IEnumerable<Pregunta>> GetAll()
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var listaPreguntas = new List<Pregunta>();
                    listaPreguntas = new PreguntaNegocio().GetAll().ToList();

                    if (listaPreguntas.Count <= 0)
                    {
                        return NoContent();
                    }
                    return Ok(listaPreguntas);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        // GET api/<PreguntaController>/5
        [HttpGet("{id}")]
        public ActionResult<Empresa> GetById(int id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var pregunta = preguntaNegocio.GetById(id);
                    if (pregunta == null)
                    {
                        return NotFound();
                    }
                    return Ok(pregunta);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        // POST api/<PreguntaController>
        [HttpPost]
        public void Post([FromBody] RespuestasCompuesta respuestasCompuesta)
        {

        }

        // PUT api/<PreguntaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PreguntaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
