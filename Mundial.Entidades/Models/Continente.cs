using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades { 
    public partial class Continente
    {
        public Continente()
        {
            Equipos = new HashSet<Equipo>();
        }

        public int IdContinente { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Equipo> Equipos { get; set; }
    }
}
