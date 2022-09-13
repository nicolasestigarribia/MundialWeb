using Mundial.Entidades;
using Mundial.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Data;
using System.Security.Claims;
using Mundial.Web.Auth;
using MudBlazor;
using System.Transactions;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mundial.Web.Pages.Views
{
	public partial class Preguntas : ComponentBase
	{
		public const int CodigoPreguntaCompuesta = 2;
		public const int CodigoPreguntaSimple = 1;
		public int UsuarioLogueado = 0;

		private bool deshabilitar = false;
		private bool seGrabo = false;

		public List<Pregunta?> ListaPreguntas { get; set; } = new List<Pregunta?>();
		public IEnumerable<Equipo> ListaEquipos { get; set; }
		public IEnumerable<Jugador> ListaJugadores { get; set; }
		public List<RespuestaSimple> ListaRtaSimple { get; set; }

		public List<RespuestaCompuestaItem> ListasRespuestasCheck = new List<RespuestaCompuestaItem>();
		public List<RespuestaCompuestaItem?> ListaRtaCompuesta { get; set; }
		public Dictionary<string, List<Equipo>> EquiposAgrupados { get; set; }



        [Inject]
		private ISessionStorageService sessionStorage { get; set; }

        [Inject]
		private IPreguntaService preguntaService { get; set; }

		[Inject]
		private IRespuestaCompuestasService rtaCompuestaService { get; set; }

		[Inject]
		private IRespuestaSimpleService rtaSimpleService { get; set; }

		[Inject]
		private IEquipoService equiposService { get; set; }

		[Inject]
		private IJugadorService jugadoresService { get; set; }

		[Inject]
		private NavigationManager navegador { get; set; }

		protected override async Task OnInitializedAsync()
		{

            UsuarioLogueado = Convert.ToInt32(await sessionStorage.GetItemAsync<string>("ID"));

            ListaPreguntas = (await preguntaService.GetAll()).ToList();
			ListaEquipos = await equiposService.GetAll();
			ListaJugadores = await jugadoresService.GetAll();
			await CargaEquiposAgrupados();

			//Traigo todas las respuestas segun el usuario logueado
			ListaRtaSimple = await rtaSimpleService.GetAllByIdUser(UsuarioLogueado);
			ListaRtaCompuesta = await rtaCompuestaService.GetAllByIdUser(UsuarioLogueado);

			if (ListaRtaSimple.Count() > 0 || ListaRtaCompuesta.Count() > 0)
			{
				deshabilitar = true;
				 await CargarRespuestas();
				StateHasChanged();
			}
		}
		private async Task CargarRespuestas()
		{
			//Agrupo por id de pregunta las respuestas compuestas, esto porque las respuestas compuestas pueden ser mas de una para una misma pregunta
			var listaIdPreguntas = ListaRtaCompuesta.Select(a => a.IdPregunta).Distinct().ToList();
			var RtaCompuestasAgrupadas = new Dictionary<int, List<RespuestaCompuestaItem>>();
			foreach (var idPregunta in listaIdPreguntas)
			{
				RtaCompuestasAgrupadas.Add(idPregunta, ListaRtaCompuesta.Where(r => r.IdPregunta == idPregunta).ToList());
			}

			//Cargo las respuestas simples
			foreach (var respuesta in ListaRtaSimple)
			{
				switch (respuesta.IdPregunta)
				{
					case 2:
						rtaSimple2 = respuesta.Respuesta;
						break;
					case 3:
						rtaSimple3 = respuesta.Respuesta;
						break;
					case 4:
						rtaSimple4 = respuesta.Respuesta;
						break;
					case 5:
						rtaSimple5 = respuesta.Respuesta;
						break;
					case 6:
						rtaSimple6 = respuesta.Respuesta;
						break;
					case 7:
						rtaSimple7 = respuesta.Respuesta;
						break;
					case 8:
						rtaSimple8 = respuesta.Respuesta;
						break;
					case 9:
						rtaSimple9 = respuesta.Respuesta;
						break;
					case 10:
						rtaSimple10 = respuesta.Respuesta;
						break;
					case 19:
						rtaSimple19 = respuesta.Respuesta;
						break;
					case 20:
						rtaSimple20 = respuesta.Respuesta;
						break;
				}
			}

			//Cargo las respuestas compuestas
			foreach (var rtaCompuesta in RtaCompuestasAgrupadas)
			{
				switch (rtaCompuesta.Key)
				{
					case 1:
						rtaCompuesta1 = rtaCompuesta.Value.FirstOrDefault().Respuesta;
						break;
					case 11:
						rtaCompuesta11 = rtaCompuesta.Value.FirstOrDefault().Respuesta;
						break;
					case 12:
						rtaCompuesta12 = rtaCompuesta.Value.FirstOrDefault().Respuesta;
						break;
					case 13:
						rtaCompuesta13 = rtaCompuesta.Value.FirstOrDefault().Respuesta;
						break;
					case 14:
						rtaCompuesta14 = rtaCompuesta.Value.FirstOrDefault().Respuesta;
						break;
					case 15:
						foreach (var respuesta in rtaCompuesta.Value)
						{
							await CargarJugadorAEquipoIdeal(ListaJugadores.FirstOrDefault(j => j.IdJugador == respuesta.Respuesta));
						}
						break;
					case 21:
						rtaCompuesta21 = rtaCompuesta.Value.FirstOrDefault().Respuesta;
						break;
					case 23:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkSenegal = true;
									break;
								case 2:
									chkPaisesBajos = true;
									break;
								case 3:
									chkQatar = true;
									break;
								case 4:
									chkEcuador = true;
									break;
							}
						}
						break;

					case 24:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{

							switch (rta.Orden)
							{
								case 1:
									chkING = true;
									break;
								case 2:
									chkIran = true;
									break;
								case 3:
									chkEEUU = true;
									break;
								case 4:
									chkGles = true;
									break;
							}
						}
						break;
					case 25:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkARG = true;
									break;
								case 2:
									chkASaudita = true;
									break;
								case 3:
									chkMEX = true;
									break;
								case 4:
									chkPOL = true;
									break;
							}
						}
						break;
					case 42:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkDINA = true;
									break;
								case 2:
									chkTNZ = true;
									break;
								case 3:
									chkAUSTRALIA = true;
									break;
								case 4:
									chkFRA = true;
									break;
							}
						}
						break;
					case 43:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkALM = true;
									break;
								case 2:
									chkJPN = true;
									break;
								case 3:
									chkESP = true;
									break;
								case 4:
									chkCRica = true;
									break;
							}
						}
						break;
					case 44:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkMarruecos = true;
									break;
								case 2:
									chkCRO = true;
									break;
								case 3:
									chkBEL = true;
									break;
								case 4:
									chkCND = true;
									break;
							}
						}
						break;
					case 45:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkSZA = true;
									break;
								case 2:
									chkCMRU = true;
									break;
								case 3:
									chkBRA = true;
									break;
								case 4:
									chkSerbia = true;
									break;
							}
						}
						break;
					case 46:
						ListasRespuestasCheck.AddRange(rtaCompuesta.Value);
						foreach (var rta in rtaCompuesta.Value)
						{
							switch (rta.Orden)
							{
								case 1:
									chkURU = true;
									break;
								case 2:
									chkCDS = true;
									break;
								case 3:
									chkPOR = true;
									break;
								case 4:
									chkGhana = true;
									break;
							}
						}
						break;

				}
			}
		}

		private async Task EliminaYGrabaRespuestas()
		{
                try
                {
					using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
					{
						await rtaSimpleService.DeleteAllByUser(UsuarioLogueado, false);
						await rtaCompuestaService.DeleteAllByUser(UsuarioLogueado);
						await GrabarRespuestas();
						scope.Complete();
					}
                }
                catch (Exception ex)
					{
					MessageBoxOptions axu = new MessageBoxOptions();
					axu.Message = ex.Message;
                }
        }

        public async Task GrabarRespuestas()
		{
			var listaRespuestasAGrabar = new List<RespuestaSimple>();
			var listaRtaCompuestasItemAGrabar = new List<RespuestaCompuestaItem>();
			foreach (Pregunta pregunta in ListaPreguntas)
			{
				var rtaSimple = new RespuestaSimple(UsuarioLogueado, pregunta.IdPregunta, pregunta.Tipo, 0, 0);
				var rtaCompuesta = new RespuestaCompuestaItem(pregunta.IdPregunta, UsuarioLogueado, pregunta.Orden, 0, 0, pregunta.Tipo);
				switch (pregunta.IdPregunta)
				{
					case 1:
						rtaCompuesta.Respuesta = rtaCompuesta1;
						listaRtaCompuestasItemAGrabar.Add(rtaCompuesta);
						break;
					case 2:
						rtaSimple.Respuesta = rtaSimple2;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 3:
						rtaSimple.Respuesta = rtaSimple3;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 4:
						rtaSimple.Respuesta = rtaSimple4;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 5:
						rtaSimple.Respuesta = rtaSimple5;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 6:
						rtaSimple.Respuesta = rtaSimple6;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 7:
						rtaSimple.Respuesta = rtaSimple7;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 8:
						rtaSimple.Respuesta = rtaSimple8;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 9:
						rtaSimple.Respuesta = rtaSimple9;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 10:
						rtaSimple.Respuesta = rtaSimple10;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 11:
						rtaCompuesta.Respuesta = rtaCompuesta11;
						listaRtaCompuestasItemAGrabar.Add(rtaCompuesta);
						break;
					case 12:
						rtaCompuesta.Respuesta = rtaCompuesta12;
						listaRtaCompuestasItemAGrabar.Add(rtaCompuesta);
						break;
					case 13:
						rtaCompuesta.Respuesta = rtaCompuesta13;
						listaRtaCompuestasItemAGrabar.Add(rtaCompuesta);
						break;
					case 14:
						rtaCompuesta.Respuesta = rtaCompuesta14;
						listaRtaCompuestasItemAGrabar.Add(rtaCompuesta);
						break;
					case 15:
						foreach (var respuesta in listaEquipoIdeal)
						{
                            var rtaPreg15 = new RespuestaCompuestaItem(pregunta.IdPregunta, UsuarioLogueado, pregunta.Orden, respuesta.IdJugador,0, pregunta.Tipo);
                            listaRtaCompuestasItemAGrabar.Add(rtaPreg15);
						}
						break;
					case 19:
						rtaSimple.Respuesta = rtaSimple19;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 20:
						rtaSimple.Respuesta = rtaSimple20;
						listaRespuestasAGrabar.Add(rtaSimple);
						break;
					case 21:
						rtaCompuesta.Respuesta = rtaCompuesta21;
						listaRtaCompuestasItemAGrabar.Add(rtaCompuesta);
						break;
					case 23:
						foreach (var respuestas in ListasRespuestasCheck)
						{
							listaRtaCompuestasItemAGrabar.Add(respuestas);
						}
						break;
				}
			}
			var cantidadRta = await rtaCompuestaService.InsertMasivo(listaRtaCompuestasItemAGrabar);
			var cantidadRtaSimples = await rtaSimpleService.InsertMasivo(listaRespuestasAGrabar);
			if (cantidadRta > 0 && cantidadRtaSimples > 0)
			{
				seGrabo = true;
			}
		}
     
        private async Task SaveAndShowSnackbarAsync()
			{
				await EliminaYGrabaRespuestas();
				Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
				if (seGrabo)
				{
					Snackbar.Add("¡ Se grabaron las respuestas correctamente !", Severity.Success);
					deshabilitar = true;
				}
				else
				{
					Snackbar.Add("¡ No se grabaron las respuestas, intentelo nuevamente !", Severity.Error);
				}
			}

		private async Task CargaListaJugadoresXEquipo(int idEquipo)
		{
			listaJugadoresxEquipo = ListaJugadores.Where(j => j.IdEquipo == idEquipo).ToList();
		}
		private async Task CargarJugadorAEquipoIdeal(Jugador jugador)
		{
			if (listaEquipoIdeal.Count() <= 11)
			{
				if (listaEquipoIdeal.Where(j => j.IdJugador == jugador.IdJugador).Count() == 0)
				{
					Equipo equipo = ListaEquipos.SingleOrDefault(e => e.IdEquipo == jugador.IdEquipo);
					listaEquipoIdeal.Add(new JugadorItem(jugador.IdJugador, jugador.Puesto, jugador.Nombre, equipo));
				}
			}
		}
		private async Task EliminarJugador(int jugadorDelete)
		{
			listaEquipoIdeal.Remove(listaEquipoIdeal.SingleOrDefault(j => j.IdJugador == jugadorDelete));
		}


		public async Task CargaEquiposAgrupados()
		{
			var ListaGrupos = ListaEquipos.Select(a => a.Grupo).Distinct().ToList();

			EquiposAgrupados = new Dictionary<string, List<Equipo>>();
		
			foreach (var grupo in ListaGrupos)
			{
				EquiposAgrupados.Add(grupo.Trim(), ListaEquipos.Where(a => a.Grupo == grupo).ToList());
			}
		}




		private async Task GrabaRespuestasPreg23(int rta, string grupo, int orden)
		{

			bool msgError = false;
			switch (grupo)
			{
				case "A":
					var rtaCompuesta = new RespuestaCompuestaItem(23,UsuarioLogueado,orden,rta,0,CodigoPreguntaCompuesta);
					var listaDeRta23 = ListasRespuestasCheck.Where(r => r.IdPregunta == 23).ToList();
					if (listaDeRta23.Count() < 2 && listaDeRta23.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkSenegal = true;
								break;
							case 2:
								chkPaisesBajos = true;
								break;
							case 3:
								chkQatar = true;
								break;
							case 4:
								chkEcuador = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkSenegal = false;
						chkPaisesBajos = false;
						chkQatar = false;
						chkEcuador = false;
						foreach (var respuesta in listaDeRta23)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
				case "B":
					rtaCompuesta = new RespuestaCompuestaItem(24, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta);
                    var listaDeRta24 = ListasRespuestasCheck.Where(r => r.IdPregunta == 24).ToList();
					if (listaDeRta24.Count() < 2 && listaDeRta24.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkING = true;
								break;
							case 2:
								chkIran = true;
								break;
							case 3:
								chkEEUU = true;
								break;
							case 4:
								chkGles = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkING = false;
						chkIran = false;
						chkEEUU = false;
						chkGles = false;
						foreach (var respuesta in listaDeRta24)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;

				case "C":
					rtaCompuesta = new RespuestaCompuestaItem(25, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta);
                    var listaDeRta25 = ListasRespuestasCheck.Where(r => r.IdPregunta == 25).ToList();
					if (listaDeRta25.Count() < 2 && listaDeRta25.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkARG = true;
								break;
							case 2:
								chkASaudita = true;
								break;
							case 3:
								chkMEX= true;
								break;
							case 4:
								chkPOL = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkARG = false;
						chkASaudita = false;
						chkMEX = false;
						chkPOL = false;
						foreach (var respuesta in listaDeRta25)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
				case "D":
					rtaCompuesta = new RespuestaCompuestaItem(42, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta); 
					var listaDeRta42 = ListasRespuestasCheck.Where(r => r.IdPregunta == 42).ToList();
					if (listaDeRta42.Count() < 2 && listaDeRta42.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkDINA = true;
								break;
							case 2:
								chkTNZ= true;
								break;
							case 3:
								chkAUSTRALIA = true;
								break;
							case 4:
								chkFRA = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkDINA = false;
						chkTNZ = false;
						chkAUSTRALIA = false;
						chkFRA = false;
						foreach (var respuesta in listaDeRta42)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
				case "E":
					rtaCompuesta = new RespuestaCompuestaItem(43, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta);
                    var listaDeRta43 = ListasRespuestasCheck.Where(r => r.IdPregunta == 43).ToList();
					if (listaDeRta43.Count() < 2 && listaDeRta43.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkALM = true;
								break;
							case 2:
								chkJPN = true;
								break;
							case 3:
								chkESP= true;
								break;
							case 4:
								chkCRica = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkALM = false;
						chkJPN = false;
						chkESP = false;
						chkCRica = false;
						foreach (var respuesta in listaDeRta43)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
				case "F":
					rtaCompuesta = new RespuestaCompuestaItem(44, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta);
                    var listaDeRta44 = ListasRespuestasCheck.Where(r => r.IdPregunta == 44).ToList();
					if (listaDeRta44.Count() < 2 && listaDeRta44.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkMarruecos = true;
								break;
							case 2:
								chkCRO = true;
								break;
							case 3:
								chkBEL = true;
								break;
							case 4:
								chkCND = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkMarruecos = false;
						chkCRO = false;
						chkBEL = false;
						chkCND = false;
						foreach (var respuesta in listaDeRta44)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
				case "G":
					rtaCompuesta = new RespuestaCompuestaItem(45, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta);
                    var listaDeRta45 = ListasRespuestasCheck.Where(r => r.IdPregunta == 45).ToList();
					if (listaDeRta45.Count() < 2 && listaDeRta45.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkSZA = true;
								break;
							case 2:
								chkCMRU = true;
								break;
							case 3:
								chkBRA = true;
								break;
							case 4:
								chkSerbia = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkSZA = false;
						chkCMRU = false;
						chkBRA = false;
						chkSerbia = false;
						foreach (var respuesta in listaDeRta45)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
				case "H":
					rtaCompuesta = new RespuestaCompuestaItem(46, UsuarioLogueado, orden, rta, 0, CodigoPreguntaCompuesta);
                    var listaDeRta46 = ListasRespuestasCheck.Where(r => r.IdPregunta == 46).ToList();
					if (listaDeRta46.Count() < 2 && listaDeRta46.Where(r => r.Respuesta == rtaCompuesta.Respuesta).Count() == 0)
					{
						switch (orden)
						{
							case 1:
								chkURU = true;
								break;
							case 2:
								chkCDS = true;
								break;
							case 3:
								chkPOR = true;
								break;
							case 4:
								chkGhana = true;
								break;
						}
						ListasRespuestasCheck.Add(rtaCompuesta);
					}
					else
					{
						chkURU = false;
						chkCDS = false;
						chkPOR = false;
						chkGhana = false;
						foreach (var respuesta in listaDeRta46)
						{
							ListasRespuestasCheck.Remove(respuesta);
						}
						msgError = true;
					}
					break;
			}

			if (msgError)
			{
				Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
				Snackbar.Add("¡ Solo se puede seleccionar 2 equipos por grupo !", Severity.Warning);
			}
        }

		private async Task GoToProdeViewAsync()
		{
			await EliminaYGrabaRespuestas();
			navegador.NavigateTo("/Prode");
        }

        private async Task GoToFormacionViewAsync()
        {
            await EliminaYGrabaRespuestas();
            navegador.NavigateTo("/Formaciones");
        }

    }
}
