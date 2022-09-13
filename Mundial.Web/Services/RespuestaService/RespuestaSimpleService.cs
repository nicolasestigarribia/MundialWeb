using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public class RespuestaSimpleService : IRespuestaSimpleService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<RespuestaSimpleService> logger;
        private string EndPoint { get; set; }

        public RespuestaSimpleService(HttpClient httpClient, ILogger<RespuestaSimpleService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            EndPoint = "api/RespuestaSimples";
        }

        public async Task<bool> Insert(RespuestaSimple respuestaNueva)
        {
            bool rta = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync<RespuestaSimple>($"{EndPoint}/", respuestaNueva);
                rta = response.StatusCode == System.Net.HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Insert -> Error al acceder al controlador", ex.Message);
            }
            return rta;
        }

        public async Task<RespuestaSimple> GetById(int idPregunta, int idUsuario)
        {
            RespuestaSimple respuestaSimple = new RespuestaSimple();
            try
            {
                respuestaSimple = await httpClient.GetFromJsonAsync<RespuestaSimple>($"{EndPoint}/{idPregunta}/{idUsuario}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetById -> Error al acceder al controlador", e.Message);
            }
            return respuestaSimple;
        }

        public async Task<IEnumerable<RespuestaSimple>> GetAll()
        {
            List<RespuestaSimple> respuestasSimpleList = new List<RespuestaSimple>();
            try
            {
                respuestasSimpleList = await httpClient.GetFromJsonAsync<List<RespuestaSimple>>($"{EndPoint}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetAll -> Error al acceder a los datos", e.Message);
            }
            return respuestasSimpleList;
        }

        public async Task<bool> DeleteAllByUser(int idUser, bool exception)
        {
            bool rta = false;
            try
            {
                var response = await httpClient.DeleteAsync($"{EndPoint}/Usuarios/{idUser}/{exception}");
                rta = response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DeleteAll -> Error al acceder al controlador ", ex.Message);
            }
            return rta;
        }

        public async Task<List<RespuestaSimple>> GetAllByIdUser(int idUsuario)
        {
            List<RespuestaSimple> respuestasSimpleList = new List<RespuestaSimple>();
            try
            {
                respuestasSimpleList = await httpClient.GetFromJsonAsync<List<RespuestaSimple>>($"{EndPoint}/{idUsuario}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetAllByID -> Error al acceder a los datos", e.Message);
            }
            return respuestasSimpleList;
        }


        public async Task<int?> InsertMasivo(List<RespuestaSimple> listaRespuestas)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<List<RespuestaSimple>>($"{EndPoint}/InsertMasivo", listaRespuestas);

                return response.StatusCode == System.Net.HttpStatusCode.OK ? response.Content.ReadFromJsonAsync<int>().Result : null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "InsertItem -> Error al acceder al controlador ", ex.Message);
            }
            return 0;
        }

        public async Task<List<RespuestaSimple>> GetAllByOrdenAndIdUser(int idUser, int orden)
        {
            List<RespuestaSimple> respuestasSimpleList = new List<RespuestaSimple>();
            try
            {
                respuestasSimpleList = await httpClient.GetFromJsonAsync<List<RespuestaSimple>>($"{EndPoint}/{idUser}/Orden/{orden}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetAllByOrdenAndIdUser -> Error al acceder a los datos", e.Message);
            }
            return respuestasSimpleList;
        }
    }
}
