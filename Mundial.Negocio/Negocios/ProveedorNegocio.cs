using Mundial.EF;
using Mundial.Entidades;
using System.Linq;

namespace Mundial.Negocio
{
    public class ProveedorNegocio : BaseNegocio<Proveedores>
    {
        public ProveedorNegocio()
        {
        }

        public bool ExisteCUIT(string cuit)
        {
            return Context.Proveedores.Count(p => p.Cuit == cuit) > 0;
        }

        public Proveedores GetByCuit(string cuit)
        {
            return Context.Proveedores.Where(a => a.Cuit.Contains(cuit.Insert(0, "0"))).SingleOrDefault();
        }
    }
}
