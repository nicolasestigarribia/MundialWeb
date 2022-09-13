using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public interface IPersonaService
	{
		Task<PerfilItem> GetById(int idPersona);

		Task<bool> Update(Persona persona);

        Task<PerfilItem> GetBylegajo(int idPersona);

        Task<PerfilItem> GetByCuit(string cuit);

    }
}
