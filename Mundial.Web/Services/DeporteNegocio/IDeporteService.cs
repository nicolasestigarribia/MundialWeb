using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public interface IDeporteService
    {
        Task<List<Deporte>> GetAll();

    }
}
