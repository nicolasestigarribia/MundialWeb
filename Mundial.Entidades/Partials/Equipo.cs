using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mundial.Entidades;

namespace Mundial.Entidades
{
    public partial class Equipo
    {
        public string NombreContinente { get { return this.IdContinenteNavigation?.Nombre ?? "Sin definir"; } }

    }
}
