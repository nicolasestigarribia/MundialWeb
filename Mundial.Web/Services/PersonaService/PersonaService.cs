using Mundial.Entidades;
using System.Net;

namespace Mundial.Web.Services
{
	public class PersonaService : IPersonaService
	{

        private readonly HttpClient httpClient;
        private readonly ILogger<PersonaService> logger;
        private string EndPoint { get; set; }
        public PersonaService(HttpClient httpClient, ILogger<PersonaService> logger)
        {
            this.httpClient = httpClient;
            EndPoint = "api/Personas";
            this.logger = logger;
        }

        public async Task<PerfilItem> GetById(int idPersona)
		{
            var rta = new PerfilItem();
            try
            {
                rta = await httpClient.GetFromJsonAsync<PerfilItem?>($"{EndPoint}/{idPersona}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById -> Error al intentar traer un usuario por id", ex.Message);
            }

            return rta;
        }

        public async Task<bool> Update(Persona persona)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                 response = await httpClient.PutAsJsonAsync($"{EndPoint}/{persona.IdPersona}", persona);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update -> Error al modificar un usuario", ex.Message);
            }

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<PerfilItem> GetBylegajo(int legajo)
        {
            var rta = new PerfilItem();
            try
            {
                rta = await httpClient.GetFromJsonAsync<PerfilItem?>($"{EndPoint}/Legajo/{legajo}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById -> Error al intentar traer un usuario por legajo", ex.Message);
            }

            return rta;
        }

        public async Task<PerfilItem> GetByCuit(string cuit)
        {
            var rta = new PerfilItem();
            try
            {
                rta = await httpClient.GetFromJsonAsync<PerfilItem?>($"{EndPoint}/Cuit/{cuit}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetById -> Error al intentar traer un usuario por legajo", ex.Message);
            }

            return rta;
        }
    }
}
