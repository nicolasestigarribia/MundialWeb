using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Mundial.Entidades;
using Mundial.Negocio;
using Mundial.Negocio.Utils;
using Mundial.Entidades.LoginModel;

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioNegocio UsuarioNegocio;
        private readonly PersonaNegocio PersonaNegocio;
        private readonly IMapper _mapper;
        private readonly ILogger<Usuario> logger;

        public UsuarioController(PersonaNegocio personalNegocio ,UsuarioNegocio usuarioNegocio, IMapper mapper, ILogger<Usuario> logger)
        {
            this.UsuarioNegocio = usuarioNegocio;
            this.logger = logger;
            this._mapper = mapper;
            this.PersonaNegocio = personalNegocio;
        }

        [HttpPost("Registro")]
        public ActionResult<Usuario> Resgitro([FromBody] UsuarioInsertRequestDto userNuevo)
        {
            bool seRegistro = false;
            if (ModelState.IsValid)
            {
                try
                {
                    if(!UsuarioNegocio.Exist(userNuevo.Nickname))
                    {
                        var usuarioMapeado = _mapper.Map<Usuario>(userNuevo);
                        var personaNueva = _mapper.Map<Persona>(userNuevo);

                        int idPersona = PersonaNegocio.Insertar(personaNueva);

                        usuarioMapeado.Activo = true;
                        personaNueva.IdHobby = 0;
                        personaNueva.IdDeporte = 0;
                        personaNueva.IdClub = 0;
                        usuarioMapeado.IdPersona = PersonaNegocio.GetLastID();

                        seRegistro = UsuarioNegocio.Registrar(userNuevo, usuarioMapeado);
                    }
                    if (seRegistro == false)
                    {
                        return BadRequest();
                    }
                    return StatusCode(201, userNuevo);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al insertar nuevo usuario a la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetById(int id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = UsuarioNegocio.GetById(id);
                    if(usuario == null)
                    {
                        return NotFound();
                    }
                    return StatusCode(200, usuario);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al buscar un usuario en la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("mail/{mail}")]
        public ActionResult<Usuario> GetByMail(string mail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = UsuarioNegocio.GetByCondition(a => a.Mail.ToLower().Contains(mail.Trim().ToLower()));
                    if (usuario == null)
                    {
                        return NotFound();
                    }
                    return StatusCode(200, usuario);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al buscar un usuario en la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Nickname/{nick}")]
        public ActionResult<Usuario> GetByNikname(string nick)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = UsuarioNegocio.GetByCondition(a => a.NickName.ToLower().Contains(nick.Trim().ToLower()));
                    if (usuario == null)
                    {
                        return NotFound();
                    }
                    return StatusCode(200, usuario);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al buscar un usuario en la BDD ", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest(ModelState);
        }

    }
}
