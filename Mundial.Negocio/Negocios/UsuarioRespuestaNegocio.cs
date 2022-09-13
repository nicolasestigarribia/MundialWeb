using Mundial.EF;
using Mundial.Entidades;

namespace Mundial.Negocio
{
	public class UsuarioRespuestaNegocio : BaseNegocio<UsuariosRespuesta>
	{
		private const int CodigoPreguntaCompuesta = 2;
		public UsuarioRespuestaNegocio()
		{
		}


		public List<UsuariosRespuesta> GetListaUsuarioRespuestaByIdUsuario(int idUsuario) {

			return Context.UsuariosRespuestas.Where(r => r.IdUsuario == idUsuario).ToList();
		}


        public bool ExisteRespuesta(int idUser, int idPreg)
		{
			return Context.UsuariosRespuestas.Where(p => p.IdPregunta == idPreg && p.IdUsuario == idUser).Count() > 0;

		}

		public List<UsuariosRespuesta> GetListaUsuariosRespuestas()
		{
			var listaRespuestas = new List<UsuariosRespuesta>();
			var listaRtaSimples = new RespuestaSimpleNegocio().GetAll();
			var listartaCompuestas = new RespuestaCompuestaNegocio().GetListaRtaCompuestaItem();

			foreach (var respuestaSimple in listaRtaSimples)
				listaRespuestas.Add(respuestaSimple);

			foreach (var respuestaCompuesta in listartaCompuestas)
				listaRespuestas.Add(respuestaCompuesta);

			return listaRespuestas;

		}


		public int ActualizaPuntajesObtenidos()
		{
			//Me traigo todas las respestas de todos los Usuarios
			List<UsuariosRespuesta> listaRespuestasDeUsuarios = GetListaUsuariosRespuestas();

			//Traigo las preguntas con sus respectivas respuestas realies
			List<Pregunta> preguntasSistema = new PreguntaNegocio().GetAll().ToList();

			// Traigo las respuestas reales de las preguntas compuestas
			List<PreguntasCompuesta> listaPreguntasCompuestas = new PreguntaCompuestaNegocio().GetAll().ToList();

			//Necestario para calcular los puntajes de la respuesta 15
			JugadorPuntaje jugadorPuntaje = new JugadorNegocio().GetPuntajes();

			List<UsuariosRespuesta> listaRespuestasAUpdatear = new List<UsuariosRespuesta>();
			int registrosActualizados = 0;
			int puntaje = 0;
			var jugadorNegocio = new JugadorNegocio();


			// Recorro la lista de respuestas de los usuarios
			foreach (var respuestaUsuario in listaRespuestasDeUsuarios)
			{
                //Busco la pregunta 
                Pregunta? pregunta = preguntasSistema.Find(pr => pr.IdPregunta == respuestaUsuario.IdPregunta);

				if (pregunta != null)
				{
					if (respuestaUsuario.Tipo == CodigoPreguntaCompuesta)
					{
						
						var urCompuesta = (RespuestaCompuestaItem)respuestaUsuario;

						if (pregunta.IdPregunta == 15)
						{
                            puntaje = jugadorPuntaje.PuntuarEquipo(jugadorNegocio.GetListaJugadoresByListaIds(urCompuesta.ListaRespuestas));

                        }else
						{
                            bool acerto = true;
                            int acertados = 0;

                            //Recorro la lista de respuestas de cada respuesta compuesta
                            foreach (var valorRespuesta in urCompuesta.ListaRespuestas)
                            {

                                //Salteo la posicion 12 de la pregunta 22, ya que en esa posicion se encuentra la formacion que no se contabiliza
                                if ((pregunta.IdPregunta != 22) || ((pregunta.IdPregunta == 22) && valorRespuesta.Orden != 12))
                                {
                                    //Si la respuesta se encuentra entre las respuestas reales, se marca como acertado
                                    if (listaPreguntasCompuestas.Where(pr => pr.IdPregunta == valorRespuesta.IdPregunta && pr.Respuesta == valorRespuesta.Respuesta).Count() > 0)
                                    {
                                        acertados++;
                                    }
                                    else
                                    {
                                        acerto = false;
                                    }
                                }
                            }

                            int porcentajeDeAcierto = 100;
                            //Esta pregunta tiene puntaje variable
                            if (pregunta.IdPregunta == 22)
                            {
                                if (acertados >= 9)
                                {
                                    if (acertados == 11)
                                        porcentajeDeAcierto = 100;
                                    if (acertados == 10)
                                        porcentajeDeAcierto = 50;
                                    if (acertados == 9)
                                        porcentajeDeAcierto = 20;
                                    acerto = true;
                                }
                            }
                            puntaje = acerto ? (int)(pregunta.Puntaje * porcentajeDeAcierto / 100) : 0;
                        }
					}
					else
					{
						var pregSimple = new PreguntaSimple(pregunta.IdPregunta, pregunta.Pregunta1, pregunta.Puntaje, pregunta.Tipo, pregunta.RespuestaSimple, pregunta.Orden);
						puntaje = pregSimple.EvaluarRespuesta(respuestaUsuario);
					}
						respuestaUsuario.Puntaje = puntaje;
						listaRespuestasAUpdatear.Add(respuestaUsuario);
						registrosActualizados++;
				}
			}
			this.UpdatePuntajes(listaRespuestasAUpdatear);
			new UsuarioNegocio().ActualizaPuntajesUsuario();
			return registrosActualizados;
		}

		public int UpdatePuntajes(List<UsuariosRespuesta> listaRespuestas)
		{
			foreach (var rta in listaRespuestas)
			{
				var respuesta = Context.UsuariosRespuestas.Where(r => r.IdPregunta == rta.IdPregunta && r.IdUsuario == rta.IdUsuario).SingleOrDefault();
				respuesta.Puntaje = rta.Puntaje;
				Context.Attach(respuesta);
			}
			return Context.SaveChanges();
		}



		public List<RankingUsuarioItem> GetRankingUsuarios()
		{

			var listaRankingUsuarios = (from UserRta in Context.UsuariosRespuestas
										join User in Context.Usuarios on new { UserRta.IdUsuario } equals new { User.IdUsuario }
										join persona in Context.Personas on new { User.IdPersona} equals new { IdPersona = persona.IdPersona}
										join empresa in Context.Empresas on new { persona.IdEmpresa } equals new { empresa.IdEmpresa }
										group new { persona, User, UserRta, UserRta.Puntaje, empresa.Nombre }
										by new
										{
											User.IdUsuario
										} into userAgrupados
										select new RankingUsuarioItem()
										{
											Usuario = userAgrupados.First().User,
											Persona = userAgrupados.First().persona,
											EmpresaNombre = userAgrupados.Select(a => a.Nombre).First(),
											PuntajeTotal = userAgrupados.Sum(a => a.Puntaje),
											CantidadDeAciertos = userAgrupados.Count(a => a.Puntaje > 0 && a.UserRta.IdUsuario == a.User.IdUsuario),
											FechaGrabacion = userAgrupados.Select(a => a.UserRta.FechaGrabacion).SingleOrDefault()

										}).OrderByDescending(a => a.PuntajeTotal).ToList();
			var pos = 1;
			listaRankingUsuarios.ForEach(a =>
			{
				a.Posicion = pos;
				pos++;
			});

			return listaRankingUsuarios;

        }


    }
}
