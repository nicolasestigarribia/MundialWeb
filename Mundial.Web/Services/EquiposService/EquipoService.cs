using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly HttpClient httpClient;

        private readonly ILogger<EquipoService> logger;

        private String EndpointRoot;

        public EquipoService(HttpClient httpClient, ILogger<EquipoService> logger)
        {
            this.httpClient = httpClient;
            EndpointRoot = "api/Equipos";
            this.logger = logger;
        }

        public async Task<IEnumerable<Equipo>> GetAll()
        {
            List<Equipo> listaEquipos = new List<Equipo>();
            try
            {
                listaEquipos = await httpClient.GetFromJsonAsync<List<Equipo>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll -> Error al acceder al endPoint : {mensaje}", ex.Message);
            }
            return listaEquipos;
        }
    }
}
