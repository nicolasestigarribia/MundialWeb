using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class EmpresaController : ControllerBase
	{

        private readonly EmpresaNegocio EmpresaNegocio;

        private readonly ILogger<Empresa> logger;

        public EmpresaController(EmpresaNegocio empresaNegocio, ILogger<Empresa> logger)
        {
            this.EmpresaNegocio = empresaNegocio;
            this.logger = logger;
        }

        // GET: api/<EmpresaController>
        [HttpGet]
		public ActionResult<List<Empresa>> GetAll()
		{
            if (ModelState.IsValid)
            {
                List<Empresa> empresa = new List<Empresa>();
                try
                {
                    empresa = EmpresaNegocio.GetAll().ToList();

                    if (empresa.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(empresa);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al acceder a la BDD", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }

		// GET api/<EmpresaController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}
	
	}
}
