using Microsoft.AspNetCore.Components;
using MudBlazor;
using Mundial.Entidades;
using Mundial.Web.Auth;
using Mundial.Web.Services;
using System.Net.Http;
using System.Security.Claims;

namespace Mundial.Web.Pages.Views
{
	public partial class Formaciones
	{
		private bool Deshabilitar = false;
		public int UsuarioLogueado = 0;
        public const int IdPregunta22 = 22;

		public Pregunta Pregunta { get; set; } = new Pregunta();
        public List<Jugador> ListaJugadores { get; set; }
		public List<Jugador> ListaArqueros { get; set; } = new List<Jugador>();
		public List<Jugador> ListaDefensores { get; set; } = new List<Jugador>();
		public List<Jugador> ListaDelanteros { get; set; } = new List<Jugador>();
		public List<Jugador> ListaVolantes { get; set; } = new List<Jugador>();
		public List<RespuestaCompuestaItem> ListaRespuestasCompuesta { get; set; } = new List<RespuestaCompuestaItem>();

		[Inject]
		private IRespuestaCompuestasService rtaCompuestaService { get; set; }

		[Inject]
		private IJugadorService IJugadoresService { get; set; }

		[Inject]
		private IPreguntaService IPreguntaService { get; set; }

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private ISessionStorageService sessionStorage { get; set; }


        protected override async Task OnInitializedAsync()
		{
			//UsuarioLogueado = Convert.ToInt32(await sessionStorage.GetItemAsync<string>("ID"));
			UsuarioLogueado = 2;
            Pregunta = await IPreguntaService.GetById(IdPregunta22);
			ListaJugadores = (await IJugadoresService.GetListaJugadoresByIdEquipo(1)).ToList();
			ListaRespuestasCompuesta = (await rtaCompuestaService.GetAll(Pregunta.IdPregunta, UsuarioLogueado)).ToList();

			if (ListaRespuestasCompuesta.Count() > 0)
			{
				CargarCampoDeJuego();
				Deshabilitar = true;
				StateHasChanged();
			}
			
			ListaArqueros = ListaJugadores.Where(j => j.Puesto == "Arquero").ToList();
			ListaDefensores = ListaJugadores.Where(j => j.Puesto == "Defensor").ToList();
			ListaDelanteros = ListaJugadores.Where(j => j.Puesto == "Delantero").ToList();
			ListaVolantes = ListaJugadores.Where(j => j.Puesto == "Volante").ToList();
		}

        private void CargarCampoDeJuego()
        {
			foreach (var rta in ListaRespuestasCompuesta)
			{
				switch (rta.Orden)
				{
					case 0:
						arquero = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 1:
						defensor1 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 2:
						defensor2 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 3:
						defensor3 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 4:
						defensor4 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 5:
						volante1 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 6:
						volante2 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 7:
						volante3 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 8:
						volante4 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 9:
						delantero1 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 10:
						Delantero2 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 11:
						Delantero3 = ListaJugadores.Where(j => j.IdJugador == rta.Respuesta).SingleOrDefault();
						break;
					case 12:
						alineacion = rta.Respuesta;
						break;
				}
			}
        }

		private async Task AgregarJugadorAListaCorrspondiente(Jugador jugador, int orden)
		{

			RespuestaCompuestaItem rtaCompuesta = new RespuestaCompuestaItem(22, UsuarioLogueado, orden, jugador.IdJugador, 0, 2);
			int flag = 0;

			//si no existe el orden, agrego el jugador
			//si existe el orden, busco el orden, lo elimino y agrego el nuevo

			if (!ListaRespuestasCompuesta.Any(r => r.Orden == orden))
			{
				if (!ListaRespuestasCompuesta.Any(j => j.Respuesta == jugador.IdJugador))
				{
					ListaRespuestasCompuesta.Add(rtaCompuesta);
				}else
                {
					flag = 1;
				}
			}
			else
			{
				var jugadorToRemove = ListaRespuestasCompuesta.Where(r => r.Orden == orden).SingleOrDefault();
				if (!ListaRespuestasCompuesta.Any(j => j.Respuesta == jugador.IdJugador))
				{
					ListaRespuestasCompuesta.Remove(jugadorToRemove);
					ListaRespuestasCompuesta.Add(rtaCompuesta);
				}else
                {
					flag = 1;
					ListaRespuestasCompuesta.Remove(jugadorToRemove);
				}
			}

            if (flag == 1)
            {
                switch (orden)
                {
                    case 1:
                        defensor1 = new Jugador();
                        break;
                    case 2:
                        defensor2 = new Jugador();
                        break;
                    case 3:
                        defensor3 = new Jugador();
                        break;
                    case 4:
                        defensor4 = new Jugador();
                        break;
                    case 5:
                        volante1 = new Jugador();
                        break;
                    case 6:
                        volante2 = new Jugador();
                        break;
                    case 7:
                        volante3 = new Jugador();
                        break;
                    case 8:
                        volante4 = new Jugador();
                        break;
                    case 9:
                        delantero1 = new Jugador();
                        break;
                    case 10:
                        Delantero2 = new Jugador();
                        break;
                    case 11:
                        Delantero3 = new Jugador();
                        break;
                }
				Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
				Snackbar.Add("¡ El jugador ya se encuentra en cancha !", Severity.Warning);
			}
		}
		private async Task GrabarFormacion()
        {
			Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
			await rtaCompuestaService.DeleteByIdPreguntaAndIdUser(22, UsuarioLogueado);

			//Creo la respuesta de la alineacion y la agrego a la lista de respuestas 
			if (ListaRespuestasCompuesta.Count == 12)
            {
				bool seGrabo = false;
				var cantRegistrosGrabados = await rtaCompuestaService.InsertMasivo(ListaRespuestasCompuesta);
				if(ListaRespuestasCompuesta.Count() == cantRegistrosGrabados)
                {
					seGrabo = true;
                }

				if (seGrabo)
				{
					Snackbar.Add("¡ Se grabo correctamente !", Severity.Success);
					Deshabilitar = true;
				}
				else
				{
					Snackbar.Add("¡ Error al grabar la formacion !", Severity.Error);
				}
			}else
            {
				Snackbar.Add("¡ Equipo incompleto, elija todas las posiciones !", Severity.Warning);
			}
		}

		private void CambioAlineacion()
		{
			var rtaAlineacion = ListaRespuestasCompuesta.Where(o => o.Orden == 12).SingleOrDefault();
			if (rtaAlineacion != null)
			{
				ListaRespuestasCompuesta.Remove(rtaAlineacion);
				ListaRespuestasCompuesta.Add(new RespuestaCompuestaItem(22, UsuarioLogueado, 12, alineacion,0,2));
			}else
            {
				ListaRespuestasCompuesta.Add(new RespuestaCompuestaItem(22, UsuarioLogueado, 12, alineacion,0,2));
			}
		}

		private void CambiarFormacion()
        {
			Deshabilitar = false;
        }
	}
}
