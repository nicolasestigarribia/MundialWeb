using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    // ESTA CLASE HEREDA DE USUSARIO RESPUESTA Y LA UTILIZO PARA PEGARLE A ESA TABLA
    // ESTO CON LA FINALIDAD DE MANTENER UN EL CODIGO MAS ENTENDIBLE A LA HORA DE CARGAR LAS RESPUESTAS
    public class RespuestaSimple : UsuariosRespuesta
	{
        public RespuestaSimple() : base()
        {
        }

        public RespuestaSimple(int idUsuario, int idPregunta, int tipo, int puntaje, int respuesta) : base(idUsuario, idPregunta, tipo, puntaje, respuesta )
        {
        }
    }
}
