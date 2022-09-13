using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public interface IPreguntaService
    {
        Task<IEnumerable<Pregunta?>> GetAll();

        Task<Pregunta> GetById(int idPregunta);

    }
}
