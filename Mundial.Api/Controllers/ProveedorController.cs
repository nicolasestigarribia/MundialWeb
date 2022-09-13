using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
	[Route("api/[controller]es")]
	[ApiController]
	public class ProveedorController : ControllerBase
	{


        private readonly ProveedorNegocio proveedorNegocio;
        private readonly ILogger<Proveedores> logger;

        public ProveedorController(ProveedorNegocio proveedorNegocio, ILogger<Proveedores> logger)
        {
            this.proveedorNegocio = proveedorNegocio;
            this.logger = logger;
        }

        // GET: api/<ProveedorController>
        [HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

        // GET api/<ProveedorController>/5
        [HttpGet("Cuit/{cuit}")]
        public ActionResult<Proveedores> GetByCuit(string cuit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var proveedor = proveedorNegocio.GetByCuit(cuit);
                    if (proveedor == null)
                    {
                        return NotFound();
                    }
                    return StatusCode(200, proveedor);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error buscar un proveedor en la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        // POST api/<ProveedorController>
        [HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<ProveedorController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ProveedorController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
