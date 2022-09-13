using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class DeporteService : IDeporteService
    {

        private readonly HttpClient _httpClient;

        private readonly ILogger<DeporteService> logger;

        private readonly string EndpointRoot;


        public DeporteService(HttpClient httpClient, ILogger<DeporteService> logger)
        {
            _httpClient = httpClient;
            EndpointRoot = "api/Deportes";
            this.logger = logger;
        }

        public async Task<List<Deporte>> GetAll()
        {
            var DeporteList = new List<Deporte>();
            try
            {
                DeporteList = await _httpClient.GetFromJsonAsync<List<Deporte>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la base de datos", ex.Message);
            }
            return DeporteList;
        }
    }
}
