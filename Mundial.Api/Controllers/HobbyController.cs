using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly HobbyNegocio HobbyNegocio;

        private readonly ILogger<Club> logger;

        public HobbyController(HobbyNegocio hobbyNegocio, ILogger<Club> logger)
        {
            this.HobbyNegocio = hobbyNegocio;
            this.logger = logger;
        }

        // GET: api/<ClubController>
        [HttpGet]
        public ActionResult<List<Hobby>> GetAll()
        {
            if (ModelState.IsValid)
            {
                List<Hobby> Hobbys = new List<Hobby>();
                try
                {
                    Hobbys = HobbyNegocio.GetAll().ToList();

                    if (Hobbys.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Hobbys);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

        // GET api/<HobbyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HobbyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HobbyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    
    }
}
