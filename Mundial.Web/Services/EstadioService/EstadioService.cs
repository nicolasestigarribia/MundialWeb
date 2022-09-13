using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class EstadioService : IEstadioService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<EstadioService> logger;

        private readonly string EndpointRoot;

        public EstadioService(HttpClient httpClient, ILogger<EstadioService> logger)
        {
            _httpClient = httpClient;
            EndpointRoot = "api/Estadios";
            this.logger = logger;
        }

        public async Task<IEnumerable<Estadio>> GetAll()
        {
            List<Estadio> estadiosList = new List<Estadio>();
            try
            {
                estadiosList = await _httpClient.GetFromJsonAsync<List<Estadio>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll -> Error al acceder al endPoint : {mensaje}", ex.Message);
            }
            return estadiosList;
        }
    }
}
