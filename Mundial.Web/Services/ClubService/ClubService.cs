using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class ClubService : IClubService
    {

        private readonly HttpClient _httpClient;

        private readonly ILogger<ClubService> logger;

        private readonly string EndpointRoot;


        public ClubService(HttpClient httpClient, ILogger<ClubService> logger)
        {
            _httpClient = httpClient;
            EndpointRoot = "api/Clubes";
            this.logger = logger;
        }

        public async Task<List<Club>> GetAll()
        {
            var clubesList = new List<Club>();
            try
            {
                clubesList = await _httpClient.GetFromJsonAsync<List<Club>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la base de datos", ex.Message);
            }
            return clubesList;
        }
    }
}
