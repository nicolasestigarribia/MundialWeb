using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public class PuntajeService : IPuntajeService
	{
        private readonly HttpClient httpClient;
        private readonly ILogger<PuntajeService> logger;
        private string EndPoint { get; set; }

        public PuntajeService(HttpClient httpClient, ILogger<PuntajeService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            EndPoint = "api/Puntajes";
        }


        public async Task<int> ActualizarPuntajes()
		{
            int rta = 0;
            try
            {
                rta = await httpClient.GetFromJsonAsync<int>($"{EndPoint}/Actualizar");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ActualizarPuntajes -> Error al acceder al controlador", ex.Message);
            }
            return rta;
        }

        public async Task<List<RankingUsuarioItem>> GetListaRankingUsuarios()
        {

            var rankingUsuarios= new List<RankingUsuarioItem>();
            try
            {
                rankingUsuarios = await httpClient.GetFromJsonAsync<List<RankingUsuarioItem>>($"{EndPoint}/Ranking");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetListaRankingUsuarios -> Error al acceder al controlador", ex.Message);
            }
            return rankingUsuarios;
        }
    }
}
