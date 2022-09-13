using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public class JugadorItem
    {
        public int IdJugador { get; set; }
        public string Puesto { get; set; }
        public string Nombre { get; set; }
        Equipo equipo { get; set; }

        public JugadorItem(int idJugador, string puesto, string nombre, Equipo equipo)
        {
            IdJugador = idJugador;
            Puesto = puesto;
            Nombre = nombre;
            this.equipo = equipo;
        }

        public string NombreEquipo { get { return equipo.Nombre != string.Empty ? equipo.Nombre : string.Empty;} }
    }
}
