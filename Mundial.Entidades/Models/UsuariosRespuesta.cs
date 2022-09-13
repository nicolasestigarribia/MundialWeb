using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class UsuariosRespuesta
    {
        public int IdPregunta { get; set; }
        public int IdUsuario { get; set; }
        public int Puntaje { get; set; }
        public int Tipo { get; set; }
        public DateTime FechaGrabacion { get; set; }
        public int Respuesta { get; set; }
        public UsuariosRespuesta()
        {
        }

        public UsuariosRespuesta(int idUsuario, int idPregunta, int tipo, int puntaje, int respuesta)
        {
            this.IdUsuario = idUsuario;
            this.IdPregunta = idPregunta;
            this.Tipo = tipo;
            this.Puntaje = puntaje;
            FechaGrabacion = DateTime.Now;
            this.Respuesta = respuesta;
        }

    }
}
