using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public class EmpresaService : IEmpresaService
	{

        private readonly HttpClient _httpClient;

        private readonly ILogger<EmpresaService> logger;

        private readonly string EndpointRoot;


        public EmpresaService(HttpClient httpClient, ILogger<EmpresaService> logger)
        {
            _httpClient = httpClient;
            EndpointRoot = "api/Empresas";
            this.logger = logger;
        }

        public async Task<List<Empresa>> GetAll()
        {
            var preguntasList = new List<Empresa>();
            try
            {
                preguntasList = await _httpClient.GetFromJsonAsync<List<Empresa>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la base de datos", ex.Message);
            }
            return preguntasList;
        }
    }
}
