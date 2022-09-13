using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public interface IPuntajeService
	{
        Task<int> ActualizarPuntajes();
        
        Task<List<RankingUsuarioItem>> GetListaRankingUsuarios();

    }
}
