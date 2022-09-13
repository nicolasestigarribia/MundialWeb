using Microsoft.AspNetCore.Components;
using MudBlazor;
using Mundial.Entidades;
using Mundial.Web.Auth;
using Mundial.Web.Services;
using System.Net.Http;
using System.Security.Claims;

namespace Mundial.Web.Pages.Views
{
	public partial class Prode
	{
		public int UsuarioLogueado = 0;
		public bool Deshabilitar = false;

		public List<Equipo> ListaEquipos { get; set; } = new List<Equipo>();
		public Dictionary<string, List<Equipo>> EquiposAgrupados { get; set; }
        private List<RespuestaSimple> ListaRespuestasGrabadasPrev { get; set; } = new List<RespuestaSimple>();

        [Inject]
		private IRespuestaSimpleService IRtaSimpleService { get; set; }

		[Inject]
		private IEquipoService IEquipoService { get; set; }

		[Inject]
		private ISessionStorageService sessionStorageService { get; set; }

		protected override async Task OnInitializedAsync()
		{
            UsuarioLogueado = Convert.ToInt32(await sessionStorageService.GetItemAsync<string>("ID"));
            ListaEquipos = (await IEquipoService.GetAll()).ToList();
			var respuestasSQL = await IRtaSimpleService.GetAllByOrdenAndIdUser(UsuarioLogueado, 26);
            await CargaEquiposAgrupados();
            await CargaGrupos();
            if (respuestasSQL.Count() > 0)
            {
                await CargarRespuestas(respuestasSQL);

            }
             await CargoListaSemiFinales();
            StateHasChanged();
        }

        private async Task CargaEquiposAgrupados()
		{
			var ListaGrupos = ListaEquipos.Select(a => a.Grupo).Distinct().ToList();

			EquiposAgrupados = new Dictionary<string, List<Equipo>>();

			foreach (var grupo in ListaGrupos)
			{
				EquiposAgrupados.Add(grupo.Trim(), ListaEquipos.Where(a => a.Grupo == grupo).ToList());
			}
		}
		private async Task CargaGrupos()
		{
			foreach (var equipo in EquiposAgrupados)
			{
				switch (equipo.Key)
				{
					case "A":
						GrupoA.AddRange(equipo.Value);
						break;
					case "B":
						GrupoB.AddRange(equipo.Value);
						break;
					case "C":
						GrupoC.AddRange(equipo.Value);
						break;
					case "D":
						GrupoD.AddRange(equipo.Value);
						break;
					case "E":
						GrupoE.AddRange(equipo.Value);
						break;
					case "F":
						GrupoF.AddRange(equipo.Value);
						break;
					case "G":
						GrupoG.AddRange(equipo.Value);
						break;
					case "H":
						GrupoH.AddRange(equipo.Value);
						break;
				}
			}
		}

		private void GrabaRespuesta(int rta, int numPregunta)
		{
			var rtaSimple = new RespuestaSimple(UsuarioLogueado, numPregunta,1,0,rta);
		
			var rtaRepetida = ListaRespuestasGrabadasPrev.SingleOrDefault(r => r.IdPregunta == rtaSimple.IdPregunta);

			if (rtaRepetida != null)
			{
                ListaRespuestasGrabadasPrev.Remove(rtaRepetida);
                ListaRespuestasGrabadasPrev.Add(rtaSimple);
			}else
			{
                ListaRespuestasGrabadasPrev.Add(rtaSimple);
			}
		}


		private async Task Save()
		{
			int? rta = null;
			if(ListaRespuestasGrabadasPrev.Count() == 32)
			{
                await IRtaSimpleService.DeleteAllByUser(UsuarioLogueado, true);
				rta = await IRtaSimpleService.InsertMasivo(ListaRespuestasGrabadasPrev);
			}
			Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
			if(rta != null)
			{
				Snackbar.Add("¡ Se grabaron las respuestas correctamente !", Severity.Success);
				Deshabilitar = true;
			}else
			{
                Snackbar.Add("¡No se pudieron grabar las respuestas correctamente, compruebe si respondio todas las llaves!", Severity.Error);
            }
		}


		/// <summary>
		/// Metodo que carga las respuestas que el usuario grabo previamente
		/// </summary>
		/// <param name="listaRespuestas"></param>
		private async Task CargarRespuestas(List<RespuestaSimple> listaRespuestas)
        {
            foreach (var rta in listaRespuestas)
            {
				switch(rta.IdPregunta)
				{
					case 26:
						rtaGrupA1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
						break;
					case 27:
						rtaGrupB2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 28:
						rtaGrupC1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 29:
						rtaGrupD2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 126:
						rtaGrupE1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 127:
						rtaGrupF2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 128:
						rtaGrupG1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 129:
						rtaGrupH2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 130:
						rtaGrupB1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 131:
						rtaGrupA2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 132:
						rtaGrupD1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 133:
						rtaGrupC2 = rta.Respuesta; 
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 134:
						rtaGrupF1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 135:
						rtaGrupE2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 136:
						rtaGrupH1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 137:
						rtaGrupG2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 138:
						ganador1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 139:
						ganador2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 140:
						ganador3 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 141:
						ganador4 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 142:
						ganador5 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 143:
						ganador6 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 144:
						ganador7 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 145:
						ganador8 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 146:
						semiFinal1 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 147:
						semiFinal2 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 148:
						semiFinal3 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 149:
						semiFinal4 = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 150:
						tercero = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 151:
						cuarto = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 152:
						campeon = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
					case 153:
						subCampeon = rta.Respuesta;
						ListaRespuestasGrabadasPrev.Add(rta);
                        break;
				}
			}
			
            Deshabilitar = listaRespuestas.Count() > 0;
        }

		private void CambiarFormacion()
		{
			Deshabilitar = false;
		}

		private async Task CargoListaSemiFinales()
        {

			ListaSemiFinal1.AddRange(GrupoC.Union(GrupoD).Union(GrupoA).Union(GrupoB));
			ListaSemiFinal2.AddRange(GrupoE.Union(GrupoF).Union(GrupoG).Union(GrupoH));
        }
	}
}
