using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mundial.Entidades;

namespace Mundial.Negocio
{
	public class RespuestaSimpleNegocio : UsuarioRespuestaNegocio
	{
		public RespuestaSimpleNegocio() : base()
		{
		}

		public bool Insert(RespuestaSimple respuesta)
		{
			if(ExisteRespuesta(respuesta.IdUsuario, respuesta.IdPregunta))
			{
				base.Delete(respuesta);
			}
			return base.Insert(respuesta);
		}

		public bool Delete(RespuestaSimple respuesta)
		{
			return base.Delete(respuesta);
		}


		public RespuestaSimple GetById(int idPregunta, int idUsuario)
		{
			var aux = (from rta in Context.UsuariosRespuestas
					   where rta.IdPregunta == idPregunta && rta.IdUsuario == idUsuario
					   select new RespuestaSimple()
					   {
						   IdPregunta = rta.IdPregunta,
						   IdUsuario = rta.IdUsuario,
						   Puntaje = rta.Puntaje,
						   Respuesta = rta.Respuesta,
						   Tipo = rta.Tipo,
						   FechaGrabacion = rta.FechaGrabacion,
					   });
			return aux.SingleOrDefault();

        }

        public List<RespuestaSimple> GetAll()
		{
			var aux = (from rta in Context.UsuariosRespuestas
					   where rta.Tipo == 1
					   select new RespuestaSimple()
					   {
						   IdPregunta = rta.IdPregunta,
						   IdUsuario = rta.IdUsuario,
						   Puntaje = rta.Puntaje,
						   Tipo = rta.Tipo,
						   Respuesta = rta.Respuesta,
						   FechaGrabacion = rta.FechaGrabacion,
					   }).ToList();
			return aux;

        }

        public List<RespuestaSimple> GetListaUsuarioRespuestasSimplesByIdUsuario(int idUser)
		{
			var aux = (from rta in Context.UsuariosRespuestas
					   where rta.Tipo == 1 && rta.IdUsuario == idUser
					   select new RespuestaSimple()
					   {
						   IdPregunta = rta.IdPregunta,
						   IdUsuario = rta.IdUsuario,
						   Puntaje = rta.Puntaje,
						   Respuesta = rta.Respuesta,
						   Tipo = rta.Tipo,
						   FechaGrabacion = rta.FechaGrabacion,
					   }).ToList();
			return aux;

        }

        public List<RespuestaSimple> GetListaRespuestaByOrden(int idUser, int orden)
        {
            var aux = (from rta in Context.UsuariosRespuestas
					   join preg in Context.Preguntas on new {rta.IdPregunta} equals new { preg.IdPregunta}
                       where rta.Tipo == 1 && rta.IdUsuario == idUser && preg.Orden == orden 
                       select new RespuestaSimple()
                       {
                           IdPregunta = rta.IdPregunta,
                           IdUsuario = rta.IdUsuario,
                           Puntaje = rta.Puntaje,
                           Respuesta = rta.Respuesta,
                           Tipo = rta.Tipo,
                           FechaGrabacion = rta.FechaGrabacion,
                       }).ToList();
            return aux;

        }




        public bool DeleteMasivo(List<UsuariosRespuesta> listaRespuestas)
		{
			foreach (var respuesta in listaRespuestas)
			{
				Context.Remove(respuesta);
			}
			return Context.SaveChanges() > 0;
		}


		public int InsertMasivo(List<RespuestaSimple> listaRespuestas)
		{
			foreach (var rta in listaRespuestas)
			{
				if (Context.UsuariosRespuestas.Where(r => r.IdPregunta == rta.IdPregunta && r.IdUsuario == rta.IdUsuario).Count() > 0)
				{
					Context.Remove(rta);
				}
				Context.Add(rta);
			}
			return Context.SaveChanges();
		}
	}
}
