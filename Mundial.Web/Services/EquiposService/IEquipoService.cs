using Mundial.Entidades;
namespace Mundial.Web.Services
{
    public interface IEquipoService
    {
        Task<IEnumerable<Equipo>> GetAll();

    }
}
