using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public class JugadorService : IJugadorService
	{

		private readonly HttpClient httpClient;

		private readonly ILogger<JugadorService> logger;

		private string EndPoint { get; set; }

		public JugadorService(HttpClient httpClient, ILogger<JugadorService> logger)
        {
            this.httpClient = httpClient;
            EndPoint = "api/Jugadores";
            this.logger = logger;
        }

        public async Task<IEnumerable<Jugador>> GetAll()
		{
			List<Jugador> jugadoresList = new List<Jugador>();
			try
			{
				jugadoresList = await httpClient.GetFromJsonAsync<List<Jugador>>($"{EndPoint}");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "GetAll -> Error al acceder al endPoint : {mensaje}", ex.Message);
			}
			return jugadoresList;
		}

        public async Task<IEnumerable<Jugador>> GetListaJugadoresByIdEquipo(int idEquipo)
        {
			List<Jugador> jugadoresList = new List<Jugador>();
			try
			{
				jugadoresList = await httpClient.GetFromJsonAsync<List<Jugador>>($"{EndPoint}/equipo/{idEquipo}");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "GetAll -> Error al acceder al endPoint : {mensaje}", ex.Message);
			}
			return jugadoresList;
		}
    }
}
