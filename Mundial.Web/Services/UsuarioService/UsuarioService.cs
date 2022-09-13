using Mundial.Entidades;
using Mundial.Entidades.LoginModel;
using System.Net.Http.Json;

namespace Mundial.Web.Services
{
	public class UsuarioService : IUsuarioService
	{
        private readonly HttpClient httpClient;
        private readonly ILogger<UsuarioService> logger;
        private string EndPoint { get; set; }
        public UsuarioService(HttpClient httpClient, ILogger<UsuarioService> logger)
        {
            this.httpClient = httpClient;
            EndPoint = "api/Usuarios";
            this.logger = logger;
        }

        public async Task<bool> Insert(UsuarioInsertRequestDto respuestaNueva)
        {
            bool rta = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync<UsuarioInsertRequestDto>($"{EndPoint}/Registro/", respuestaNueva);
                rta = response.StatusCode == System.Net.HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Insert -> Error al insertar un nuevo usuario", ex.Message);
            }
            return rta;
        }

        public async Task<Usuario> GetById(int id)
        {
            var rta = new Usuario();
            
            try
            {
                 rta = await httpClient.GetFromJsonAsync<Usuario?>($"{EndPoint}/{id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById -> Error al intentar traer un usuario", ex.Message);
            }
            
            return rta;
        }

        public async Task<Usuario> GetByEmail(string mail)
        {
            var rta = new Usuario();

            try
            {
                rta = await httpClient.GetFromJsonAsync<Usuario?>($"{EndPoint}/mail/{mail}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById -> Error al intentar traer un usuario", ex.Message);
            }

            return rta;
        }

        public async Task<Usuario> GetByNickName(string nick)
        {
            var rta = new Usuario();

            try
            {
                rta = await httpClient.GetFromJsonAsync<Usuario?>($"{EndPoint}/NickName/{nick}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById -> Error al intentar traer un usuario", ex.Message);
            }

            return rta;
        }
    }
}
