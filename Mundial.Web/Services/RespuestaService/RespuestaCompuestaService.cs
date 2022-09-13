using Mundial.Entidades;
using System.Web;
using Microsoft.AspNetCore.Mvc.Core;
using System.Net.Http.Json;

namespace Mundial.Web.Services
{
	public class RespuestaCompuestaService : IRespuestaCompuestasService
	{

		private readonly HttpClient httpClient;
		private readonly ILogger<RespuestaCompuestaService> logger;
		private string EndPoint { get; set; }

		public RespuestaCompuestaService(HttpClient httpClient, ILogger<RespuestaCompuestaService> logger)
		{
			this.httpClient = httpClient;
			this.logger = logger;
			this.EndPoint = "api/RespuestaCompuestas";
		}

		public async Task<IEnumerable<RespuestaCompuestaItem>> GetAll(int idPregunta, int idUsuario)
		{
			var respuestasCompuestaList = new List<RespuestaCompuestaItem>();
			try
			{
				respuestasCompuestaList = await httpClient.GetFromJsonAsync<List<RespuestaCompuestaItem>>($"{EndPoint}/{idUsuario}/{idPregunta}");
			}
			catch (Exception e)
			{
				logger.LogError(e, "GetAll -> Error al acceder a los datos", e.Message);
			}
			return respuestasCompuestaList;
		}


		public async Task<bool> Insert(RespuestaCompuestaItem respuestaNueva)
		{

				bool rta = false;
				respuestaNueva.ListaRespuestas = new List<RespuestasCompuesta>();
				try
				{
					var response = await httpClient.PostAsJsonAsync<RespuestaCompuestaItem>($"{EndPoint}/", respuestaNueva);
					rta = response.StatusCode == System.Net.HttpStatusCode.Created;
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "InsertItem -> Error al acceder al controlador ", ex.Message);
				}
			return rta;
		}


		/// <summary>
		/// Metodo que inserta una lista de respuestas compuestas masivamente 
		/// </summary>
		/// <param name="listaRespuestas"></param>
		/// <returns>La cantidad de registros agregados</returns>
        public async Task<int?> InsertMasivo(List<RespuestaCompuestaItem> listaRespuestas)
		{
			var listaModificada = listaRespuestas.Select(x => { x.ListaRespuestas = new List<RespuestasCompuesta>(); return x; }).ToList();
            try
            {
                var response = await httpClient.PostAsJsonAsync<List<RespuestaCompuestaItem>>($"{EndPoint}/InsertMasivo", listaRespuestas);
               
				return response.StatusCode == System.Net.HttpStatusCode.OK ?  response.Content.ReadFromJsonAsync<int>().Result : null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "InsertItem -> Error al acceder al controlador ", ex.Message);
            }
            return 0;
        }

		/// <summary>
		/// Metodo que elimina todas las respuestas compuestas de un usuario pasado por parametro
		/// </summary>
		/// <param name="idUser"></param>
		/// <returns>True = si elimino correctamente // False = si no pudo eliminar las respuestas</returns>
        public async Task<bool> DeleteAllByUser(int idUser)
        {
			bool rta = false;
			try
			{
				var response = await httpClient.DeleteAsync($"{EndPoint}/Usuarios/{idUser}");
				rta = response.StatusCode == System.Net.HttpStatusCode.NoContent;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "InsertItem -> Error al acceder al controlador ", ex.Message);
			}
			return rta;
		}


        public async Task<List<RespuestaCompuestaItem?>> GetAllByIdUser(int idUsuario)
        {
			var respuestasCompuestaList = new List<RespuestaCompuestaItem>();
			try
			{
				respuestasCompuestaList = await httpClient.GetFromJsonAsync<List<RespuestaCompuestaItem>>($"{EndPoint}/Usuarios/{idUsuario}");
			}
			catch (Exception e)
			{
				logger.LogError(e, "GetAll -> Error al acceder a los datos", e.Message);
			}
			return respuestasCompuestaList;
		}

        /// <summary>
        /// Metodo que elimina toas las respuestas compuestas de un usuario segun la pregunta pasada por parametro
        /// </summary>
        /// <param name="idPregunta"></param>
        /// <returns>True = si elimino correctamente // False = si no pudo eliminar</returns>
        public async Task<bool> DeleteByIdPreguntaAndIdUser(int idPregunta, int IdUser)
        {
			bool rta = false;
			try
			{
				var response = await httpClient.DeleteAsync($"{EndPoint}/Pregunta/{idPregunta}/{IdUser}");
				rta = response.StatusCode == System.Net.HttpStatusCode.NoContent;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "InsertItem -> Error al acceder al controlador ", ex.Message);
			}
			return rta;
		}

	}
}
