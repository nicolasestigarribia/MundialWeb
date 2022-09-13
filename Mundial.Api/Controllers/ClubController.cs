using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]es")]
    [ApiController]
    public class ClubController : ControllerBase
    {

        private readonly ClubNegocio ClubNegocio;

        private readonly ILogger<Club> logger;

        public ClubController(ClubNegocio clubNegocio, ILogger<Club> logger)
        {
            this.ClubNegocio = clubNegocio;
            this.logger = logger;
        }

        // GET: api/<ClubController>
        [HttpGet]
		public ActionResult<List<Club>> GetAll()
        {
            if (ModelState.IsValid)
            {
                List<Club> Clubes = new List<Club>();
                try
                {
                    Clubes = ClubNegocio.GetAll().ToList();

                    if (Clubes.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Clubes);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        // GET api/<ClubController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClubController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClubController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClubController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
