using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public interface IEmpresaService
	{
		Task<List<Empresa>> GetAll();
	}
}
