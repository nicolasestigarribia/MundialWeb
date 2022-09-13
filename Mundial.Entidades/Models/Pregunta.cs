using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class Pregunta
    {
        public int IdPregunta { get; set; }
        public string Pregunta1 { get; set; }
        public int Puntaje { get; set; }
        public int Tipo { get; set; }
        public int Orden { get; set; }

        public int RespuestaSimple { get; set; }

        public Pregunta(int idPregunta, string pregunta1, int puntaje, int tipo, int orden)
        {
            IdPregunta = idPregunta;
            Pregunta1 = pregunta1;
            Puntaje = puntaje;
            Tipo = tipo;
            Orden = orden;
        }

        public Pregunta()
        {
        }

        /// <summary>
        /// Evalúa y puntúa una respuesta dada por el usuario.
        /// </summary>
        /// <param name = "ur" > UsuarioRespuestaEntity respuesta del usuario</param>
        /// <returns>Puntaje obtenido por el Usuario</returns>
        public virtual int EvaluarRespuesta(UsuariosRespuesta ur)
        {
            throw new NotImplementedException();
        }

    }
}
