using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class RespuestasCompuesta
    {
        public int IdPregunta { get; set; }
        public int Respuesta { get; set; }
        public int Orden { get; set; }
        public int IdUsuario { get; set; }

    }
}
