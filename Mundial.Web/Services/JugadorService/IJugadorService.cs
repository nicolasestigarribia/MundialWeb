using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public interface IJugadorService
    {
        Task<IEnumerable<Jugador>> GetAll();

        Task<IEnumerable<Jugador>> GetListaJugadoresByIdEquipo(int idEquipo);
    }
}
