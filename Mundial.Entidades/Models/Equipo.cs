using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class Equipo
    {
        public int IdEquipo { get; set; }
        public int IdContinente { get; set; }
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        public string Tecnico { get; set; }
        public string Grupo { get; set; }
        public string Imagen { get; set; }

        public virtual Continente IdContinenteNavigation { get; set; }


    }
}
