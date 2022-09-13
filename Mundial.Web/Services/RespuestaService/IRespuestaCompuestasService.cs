using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public interface IRespuestaCompuestasService
	{
		Task<IEnumerable<RespuestaCompuestaItem>> GetAll(int idPregunta, int idUsuario);


		Task<List<RespuestaCompuestaItem?>> GetAllByIdUser(int idUsuario);

		Task<bool> Insert(RespuestaCompuestaItem respuestaNueva);

		Task<bool> DeleteAllByUser(int idUser);

		Task<bool> DeleteByIdPreguntaAndIdUser(int idPregunta, int idUsuario);

		Task<int?> InsertMasivo(List<RespuestaCompuestaItem> listaRespuestas);


    }
}
