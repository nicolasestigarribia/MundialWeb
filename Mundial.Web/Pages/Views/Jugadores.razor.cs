using Microsoft.AspNetCore.Components;
using Mundial.Entidades;
using Mundial.Web.Services;

namespace Mundial.Web.Pages.Views
{
    public partial class Jugadores
    {

        const int IdSenegal = 1;

        public IEnumerable<Jugador> ListaJugadores { get; set; }

        [Inject]
        private IJugadorService IJugadoresService { get; set; }

        [Inject]
        private IEquipoService IEquipoService { get; set; }

        public Equipo EquipoElejido { get; set; }

        public IEnumerable<Equipo> ListaEquipos { get; set; }
        public IEnumerable<Jugador> ListaJugadoresByIdEquipo { get; set; }



        protected override async Task OnInitializedAsync()
        {
            ListaEquipos = new List<Equipo>();
            ListaEquipos = await IEquipoService.GetAll();
            await JugadoresXequipoByEquipo(IdSenegal);
        }

        private async Task JugadoresXequipoByEquipo(int idEquipo)
        {
           EquipoElejido = ListaEquipos.SingleOrDefault(e => e.IdEquipo == idEquipo);
           ListaJugadoresByIdEquipo = await IJugadoresService.GetListaJugadoresByIdEquipo(idEquipo);
        }

    }
}
