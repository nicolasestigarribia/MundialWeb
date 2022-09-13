using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public class PreguntasCompuestaItem : Pregunta
    {

        public List<short> Respuestas { get; set; }


        public PreguntasCompuestaItem(short idPregunta, string pregunta, short tipo,short puntaje, short ordenPregunta) : base(idPregunta, pregunta, tipo, puntaje, ordenPregunta)
        {

            this.Respuestas = new List<short>();
        }

        public PreguntasCompuestaItem()
        {
        }

        public override int EvaluarRespuesta(UsuariosRespuesta ur)
        {
            throw new NotImplementedException();
        }
    }
}
