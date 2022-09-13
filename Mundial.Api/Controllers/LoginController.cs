using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mundial.Entidades;
using Mundial.Entidades.LoginModel;
using Mundial.Negocio;
using Mundial.Negocio.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mundial.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<Usuario> _logger;
        private readonly UsuarioNegocio usuarioNegocio;

        public LoginController(ILogger<Usuario> logger, IConfiguration configuration, UsuarioNegocio usuarioNegocio)
        {
            this._logger = logger;
            this.usuarioNegocio = usuarioNegocio;
            this.configuration = configuration;
        }

        // POST api/Login
        [HttpPost]
        public ActionResult Login([FromBody] LoginRequestDto userLogin)
        {
            if(ModelState.IsValid)
            {
                try{
                    Usuario usuario = usuarioNegocio.GetByCondition(a => a.Mail == userLogin.Nickname || a.NickName == userLogin.Nickname );
                    if (usuario != null)
                    {
                        if (PasswordHash.Verify(userLogin.Password, usuario.PasswordHash, usuario.PasswordSalt))
                        {
                            return Ok(new { token = GenerateToken(usuario) });
                        }
                    }
                    return StatusCode(401, "No Autenticado");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al ingresar al sistema", ex.Message);
                    return StatusCode(500, ModelState);
                }
            }
            return BadRequest();
        }


        /// <summary>
        /// Metodo que setea lo claims del usuario y genera el token de acceso
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private string GenerateToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , usuario.IdPersona.ToString()),
                new Claim(ClaimTypes.Name , usuario.NickName),
                new Claim(ClaimTypes.Role , usuario.TipoUsuario.ToString()),
                new Claim(ClaimTypes.Actor, usuario.IdUsuario.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(24);

            JwtSecurityToken token = new JwtSecurityToken(
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
