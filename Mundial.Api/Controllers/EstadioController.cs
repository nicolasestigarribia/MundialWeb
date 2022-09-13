using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mundial.Entidades;
using Mundial.Negocio;

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class EstadioController : ControllerBase
    {
        private readonly EstadioNegocio EstadioNegocio;

        public EstadioController(EstadioNegocio estadioNegocio) 
        {
            EstadioNegocio = estadioNegocio;
        }

        [HttpGet]
        public IActionResult GetAllAsync() 
        {
            return Ok(EstadioNegocio.GetAll());
        }

    }
}
