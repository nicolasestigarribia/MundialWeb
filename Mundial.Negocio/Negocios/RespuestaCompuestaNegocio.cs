using Microsoft.EntityFrameworkCore;
using Mundial.EF;
using Mundial.Entidades;
using System.Collections;

namespace Mundial.Negocio
{
	public class RespuestaCompuestaNegocio : BaseNegocio<RespuestasCompuesta>
	{
		private const int CodigoRtaCompuesta = 2;

		public RespuestaCompuestaNegocio()
		{
		}

		public RespuestaCompuestaItem GetByIdItem(int idPregunta, int idUsuario)
		{
			var query = (from rta in Context.RespuestasCompuestas
						 join userRta in Context.UsuariosRespuestas on new { rta.IdUsuario, rta.IdPregunta } equals new { userRta.IdUsuario, userRta.IdPregunta }
						 where userRta.IdUsuario == idUsuario && userRta.IdPregunta == idPregunta
						 orderby userRta.IdUsuario, userRta.IdPregunta, rta.Orden
						 select new RespuestaCompuestaItem()
						 {
							 IdPregunta = userRta.IdPregunta,
							 IdUsuario = userRta.IdUsuario,
							 Orden = rta.Orden,
							 Puntaje = userRta.Puntaje,
							 Respuesta = rta.Respuesta
						 });

			return query.SingleOrDefault();

		}

		/// <summary>
		/// Devuelve una lista de respuestas compuestas agrupadas por IdPregunta y IdUsuario, con su correspondientes respuestas en forma de lista
		/// </summary>
		/// <returns></returns>
		public List<RespuestaCompuestaItem> GetListaRtaCompuestaItem()
		{
			var query = (from rta in Context.RespuestasCompuestas
						 join userRta in Context.UsuariosRespuestas on new { rta.IdUsuario, rta.IdPregunta } equals new { userRta.IdUsuario, userRta.IdPregunta }
						 where userRta.Tipo == 2
						 group new { rta, rta.Orden, userRta.Tipo }
				by new
				{
					rta.IdPregunta,
					rta.IdUsuario
				}
						 into rtaAgrupada
						 select new RespuestaCompuestaItem()
						 {
							 IdPregunta = rtaAgrupada.Key.IdPregunta,
							 IdUsuario = rtaAgrupada.Key.IdUsuario,
							 ListaRespuestas = rtaAgrupada.Select(a => a.rta).ToList(),
							 Orden = rtaAgrupada.Select(a => a.Orden).SingleOrDefault(),
							 Tipo = rtaAgrupada.Select(a => a.Tipo).SingleOrDefault(),
						 });

			var aux = query.ToList();

			return aux;
		}


		public bool EliminarItem(RespuestasCompuesta respuestaCompuestaDelete)
        {
			return Delete(respuestaCompuestaDelete);
		}

		/// <summary>
		/// Inserta un RespuestaCompuestaItem en ambas tablas, tanto en UserRespuesta como en RespuestaCompuesta, si ya existe elimina la respuesta y la vuelve e insertar
		/// /// </summary>
		/// <param name="rtaCompuestaItemNuevo"></param>
		/// <returns></returns>
		public bool InsertItem(RespuestaCompuestaItem rtaCompuestaItemNuevo)
		{
			return Insert(new RespuestasCompuesta(rtaCompuestaItemNuevo.IdPregunta, rtaCompuestaItemNuevo.Respuesta, rtaCompuestaItemNuevo.Orden, rtaCompuestaItemNuevo.IdUsuario)); 
		}

		public int InsertMasivo(List<RespuestaCompuestaItem> listaRespuestaCompuesta)
		{
           
            foreach (var rtaCompuesta in listaRespuestaCompuesta)
            {
                var rta = new RespuestasCompuesta(rtaCompuesta.IdPregunta, rtaCompuesta.Respuesta, rtaCompuesta.Orden, rtaCompuesta.IdUsuario);

				if (Context.RespuestasCompuestas.Where(r => r.IdPregunta == rta.IdPregunta && r.IdUsuario == rta.IdUsuario && r.Respuesta == rta.Respuesta).Count() > 0)
				{
					Context.RespuestasCompuestas.Remove(rta);
				}
				Context.RespuestasCompuestas.Add(rta);
			}
			return Context.SaveChanges();
		}

		/// <summary>
		/// Realiza la modificacion de una respuesta compuesta en ambas tamblas, tanto en la tabla UsuarioRespuesta como en RespuestaCompuesta
		/// </summary>
		/// <param name="rtaUpdate"></param>
		/// <returns></returns>
		public bool Updateitem(RespuestaCompuestaItem rtaUpdate)
		{
			return Update(new RespuestasCompuesta(rtaUpdate.IdPregunta, rtaUpdate.Respuesta, rtaUpdate.Orden, rtaUpdate.IdUsuario));
		}

		/// <summary>
		/// Retorno la lista de respuestas compuestas segun el id del usuario pasado por parametro
		/// </summary>
		/// <param name="idUsuario"></param>
		/// <returns></returns>
		public List<RespuestasCompuesta> GetAllByIdUsuario(int idUsuario)
		{

			return Context.RespuestasCompuestas.Where(r => r.IdUsuario == idUsuario).ToList();
		}

		/// <summary>
		/// Retorno la lista de respuestas compuestas segun el id de la pregunta pasada por parametro
		/// </summary>
		/// <param name="idPregunta"></param>
		/// <returns></returns>
		public List<RespuestasCompuesta> GetAllByIdPreguntaAndIdUser(int idPregunta, int idUsuario)
		{

			return Context.RespuestasCompuestas.Where(r => r.IdPregunta == idPregunta && r.IdUsuario == idUsuario).ToList();

		}

		/// <summary>
		/// Metodo que eliminar una lista de respuestas del contexto y luego lo refleja en la BDD
		/// </summary>
        public bool DeleteMasivo(List<RespuestasCompuesta> listaRespuestas)
        {
            foreach (var respuesta in listaRespuestas)
            {
                Context.Remove(respuesta);
            }
            return Context.SaveChanges() > 0;
        }

        public List<RespuestasCompuesta> GetListaRespuestaCompuestaByIdUsuarioPorOrden(int idUsuario){

			var query = (from rta in Context.RespuestasCompuestas
						 join userRta in Context.UsuariosRespuestas on new { rta.IdUsuario, rta.IdPregunta } equals new { userRta.IdUsuario, userRta.IdPregunta }
						 where userRta.IdUsuario == idUsuario && userRta.Tipo == 2
						 orderby userRta.IdPregunta, rta.Orden
						 select new RespuestasCompuesta()
						 {
							 IdPregunta = userRta.IdPregunta,
							 IdUsuario = userRta.IdUsuario,
							 Orden = rta.Orden,
						 });

			return query.ToList();

        }

        public bool ExisteRespuesta(int idUser, int idPreg)
		{
			return Context.UsuariosRespuestas.Where(p => p.IdPregunta == idPreg && p.IdUsuario == idUser).Count() > 0;

        }

        public List<RespuestaCompuestaItem> GetAllRtaCompuestaItemById(int idUsuario, int idPreg)
        {

			var listaRta = (from rta in Context.RespuestasCompuestas
							join userRta in Context.UsuariosRespuestas on new { rta.IdUsuario, rta.IdPregunta } equals new { userRta.IdUsuario, userRta.IdPregunta }
							where userRta.Tipo == CodigoRtaCompuesta && userRta.IdUsuario == idUsuario && userRta.IdPregunta == idPreg
							select new RespuestaCompuestaItem()
							{
								IdPregunta = userRta.IdPregunta,
								IdUsuario = userRta.IdUsuario,
								Respuesta = rta.Respuesta,
								FechaGrabacion = userRta.FechaGrabacion,
								Orden = rta.Orden,
								Puntaje = userRta.Puntaje,
								Tipo = userRta.Tipo,
							}).ToList();

			return listaRta;

		}

        public List<RespuestaCompuestaItem> GetAllRtaCompuestaItemByUsuario(int idUsuario)
        {

			var listaRta = (from rta in Context.RespuestasCompuestas
							join userRta in Context.UsuariosRespuestas on new { rta.IdUsuario, rta.IdPregunta } equals new { userRta.IdUsuario, userRta.IdPregunta }
							where userRta.Tipo == CodigoRtaCompuesta && userRta.IdUsuario == idUsuario
							select new RespuestaCompuestaItem()
							{
								IdPregunta = userRta.IdPregunta,
								IdUsuario = userRta.IdUsuario,
								Respuesta = rta.Respuesta,
								FechaGrabacion = userRta.FechaGrabacion,
								Orden = rta.Orden,
								Puntaje = userRta.Puntaje,
								Tipo = userRta.Tipo,
							}).ToList();

			return listaRta;

        }




    }
}
