using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class PreguntaService : IPreguntaService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<PreguntaService> logger;

        private readonly string EndpointRoot;


        public PreguntaService(HttpClient httpClient, ILogger<PreguntaService> logger)
        {
            _httpClient = httpClient;
            EndpointRoot = "api/Preguntas";
            this.logger = logger;
        }

        public async Task<IEnumerable<Pregunta?>> GetAll()
        {
            var preguntasList = new List<Pregunta>();
            try
            {
                preguntasList = await _httpClient.GetFromJsonAsync<List<Pregunta>>($"{EndpointRoot}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la base de datos", ex.Message);
            }
            return preguntasList;
        }


        public async Task<Pregunta> GetById(int idPregunta)
        {
            var pregunta = new PreguntaSimple();
            try
            {
                pregunta = await _httpClient.GetFromJsonAsync<PreguntaSimple>($"{EndpointRoot}/{idPregunta}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al acceder a la base de datos", ex.Message);
            }
            return pregunta;
        }

    }
}
