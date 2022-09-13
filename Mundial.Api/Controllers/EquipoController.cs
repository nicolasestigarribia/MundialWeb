using Microsoft.AspNetCore.Mvc;
using Mundial.Negocio;
using Mundial.Entidades;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ILogger<Equipo> logger;
        private readonly EquipoNegocio equipoNegocio;

        public EquipoController(EquipoNegocio equipoNegocio , ILogger<Equipo> logger)
        {
            this.equipoNegocio = equipoNegocio;
            this.logger = logger;
        }
        // GET: api/<Equipos>
        [HttpGet]
        public IActionResult GetAllAsync()
        {
            List<Equipo> equipoList = new List<Equipo>();
            try
            {
                if(ModelState.IsValid)
                {
                    equipoList = equipoNegocio.GetAll().ToList();
                    if (equipoList.Count > 0)
                    {
                        return Ok(equipoList);
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Equipos -> GetAll -> Error al consultar BD {mensaje}", e.Message);
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        // GET api/<Equipos>/5
        [HttpGet("{id}")]
        public ActionResult<Equipo> GetById(int id)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    Equipo equipo = new Equipo();
                    equipo = equipoNegocio.GetById(id);
                    if(equipo != null)
                    {
                        return Ok(equipo);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Equipos -> GetById -> Error al consultar BDD : {mensaje}", ex.Message);
                    StatusCode(500, ModelState);
                }
            }
            return NotFound();      
        }

        // POST api/<EquipoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<EquipoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Jugador value)
        {

        }

        // DELETE api/<EquipoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
