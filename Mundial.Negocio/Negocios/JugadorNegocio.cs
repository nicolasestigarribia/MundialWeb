using Mundial.EF;
using Mundial.Entidades;

namespace Mundial.Negocio
{
    public class JugadorNegocio : BaseNegocio<Jugador>
    {
        public JugadorNegocio()
        {
        }


        /// <summary>
        /// Traigo el registro con los puntajes correspondientes
        /// </summary>
        /// <returns></returns>
        public JugadorPuntaje GetPuntajes()
        {
            return Context.JugadorPuntajes.FirstOrDefault();
        }

        /// <summary>
        /// Devuelve una lista de jugadores cuayo id se encuentren en la lista pasada
        /// </summary>
        /// <param name="listaIdsJugadores"></param>
        /// <returns></returns>
        public List<Jugador> GetListaJugadoresByListaIds(List<RespuestasCompuesta> listaIdsJugadores)
        {
            var listaJugadores = new List<Jugador>();
            foreach (var rta in listaIdsJugadores)
            {
                listaJugadores.Add(GetById(rta.Respuesta));
            }
            return listaJugadores;
        }


       public List<Jugador> GetListaJugadoresByIdEquipo(int idEquipo)
        {
            return Context.Jugadores.Where(j => j.IdEquipo == idEquipo).ToList();

        }

        public List<JugadorPuntajeItem> GetListaJugadorPuntaje(List<int> listaOnceIdeal)
        {
            var listaJugadoresPuntajesItems = new List<JugadorPuntajeItem>();
            var puntajes = this.GetPuntajes();
            foreach (var idJugador in listaOnceIdeal)
            {
                listaJugadoresPuntajesItems.Add(new JugadorPuntajeItem(this.GetById(idJugador), puntajes));
            }
            return listaJugadoresPuntajesItems;
        }
    }
}
