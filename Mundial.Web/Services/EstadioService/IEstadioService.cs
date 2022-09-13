using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public interface IEstadioService
    {
        Task<IEnumerable<Estadio>> GetAll();

    }
}
