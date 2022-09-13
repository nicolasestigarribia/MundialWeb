using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class Estadio
    {
        public int IdEstadio { get; set; }
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }
}
