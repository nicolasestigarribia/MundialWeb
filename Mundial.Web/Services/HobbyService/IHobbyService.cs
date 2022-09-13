using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public interface IHobbyService
    {
        Task<List<Hobby>> GetAll();
    }
}
