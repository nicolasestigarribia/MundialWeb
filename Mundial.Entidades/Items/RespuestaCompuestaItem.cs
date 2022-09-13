using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public class RespuestaCompuestaItem : UsuariosRespuesta
    {
        public int Orden { get; set; }

        public List<RespuestasCompuesta> ListaRespuestas { get; set; }

        public RespuestaCompuestaItem(int idPregunta, int idUsuario, int orden, int respuesta, int puntaje, int tipo) : base(idUsuario, idPregunta, tipo, puntaje, respuesta)
        {
            this.Orden = orden;
            ListaRespuestas = new List<RespuestasCompuesta>();
        }

        public RespuestaCompuestaItem()
        {
        }
    }
}
