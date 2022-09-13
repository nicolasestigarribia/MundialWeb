using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public partial class RespuestasCompuesta
    {
        public RespuestasCompuesta()
        {
        }

		public RespuestasCompuesta(int idPregunta, int respuesta, int orden, int idUsuario)
		{
			IdPregunta = idPregunta;
			Respuesta = respuesta;
			Orden = orden;
			IdUsuario = idUsuario;
		}

		//public List<int> Respuestas { get; set; }

	}
}
