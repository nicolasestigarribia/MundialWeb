using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public class ProveedorService : IProveedorService
	{

        private readonly HttpClient httpClient;
        private readonly ILogger<ProveedorService> logger;
        private string EndPoint { get; set; }
        public ProveedorService(HttpClient httpClient, ILogger<ProveedorService> logger)
        {
            this.httpClient = httpClient;
            EndPoint = "api/Proveedores";
            this.logger = logger;
        }

        public async Task<Proveedores> GetByCuit(string cuit)
		{
            var rta = new Proveedores();
            try
            {
                rta = await httpClient.GetFromJsonAsync<Proveedores?>($"{EndPoint}/Cuit/{cuit}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetByCuit -> Error al intentar traer un proveedor", ex.Message);
            }
            return rta;
        }
	}
}
