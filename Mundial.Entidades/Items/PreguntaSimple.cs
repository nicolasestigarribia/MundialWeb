using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public partial class PreguntaSimple : Pregunta
    {

        public int Respuesta { get; set; }

        public PreguntaSimple(int idPregunta, string pregunta, int tipo, int puntaje, int respuesta, int ordenPregunta)
            : base(idPregunta, pregunta, tipo, puntaje, ordenPregunta)
        {
            Respuesta = respuesta;
        }

        public PreguntaSimple()
        {
        }

        public override int EvaluarRespuesta(UsuariosRespuesta ur)
        {
            return ((RespuestaSimple)ur).Respuesta.Equals(Respuesta) ? this.Puntaje : 0;
        }

    }
}
