using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class HobbyService : IHobbyService
    {

        private readonly HttpClient _httpClient;

        private readonly ILogger<HobbyService> logger;

        private readonly string EndpointRoot;


        public HobbyService(HttpClient httpClient, ILogger<HobbyService> logger)
        {
            _httpClient = httpClient;
            EndpointRoot = "api/Hobbys";
            this.logger = logger;
        }

        public async Task<List<Hobby>> GetAll()
        {
            var DeporteList = new List<Hobby>();
            try
            {
                DeporteList = await _httpClient.GetFromJsonAsync<List<Hobby>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la base de datos", ex.Message);
            }
            return DeporteList;
        }
    }
}
