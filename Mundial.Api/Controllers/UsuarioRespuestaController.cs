using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class UsuarioRespuestaController : ControllerBase
	{

        private readonly UsuarioRespuestaNegocio usuarioRespuestaNegocio;

        private readonly ILogger<UsuarioRespuestaController> logger;

		public UsuarioRespuestaController(UsuarioRespuestaNegocio usuarioRespuestaNegocio, ILogger<UsuarioRespuestaController> logger)
		{
			this.usuarioRespuestaNegocio = usuarioRespuestaNegocio;
			this.logger = logger;
		}


		// GET: api/<UsuarioRespuestaController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<UsuarioRespuestaController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<UsuarioRespuestaController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<UsuarioRespuestaController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UsuarioRespuestaController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

	}
}
