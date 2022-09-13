using Mundial.Entidades;

namespace Mundial.Web.Services
{
    public interface IClubService
    {
        Task<List<Club>> GetAll();

    }
}
