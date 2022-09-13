using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class Fase
    {
        public int IdFase { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
