using Mundial.Entidades;

namespace Mundial.Web.Services
{ 
	public interface IProveedorService
	{
        Task<Proveedores> GetByCuit(string cuit);
    }
}
