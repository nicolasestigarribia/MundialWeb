using Microsoft.AspNetCore.Components;
using MudBlazor;
using Mundial.Entidades;
using Mundial.Web.Services;
using TLogger.Model;

namespace Mundial.Web.Pages.Views
{
	public partial class Ranking
	{
		string buscador = string.Empty;
		RankingUsuarioItem selecciconado;
        public int CantPaginasTotales { get; set; } = 0;
        public int CantAMostrar { get; set; } = 50;

        List<RankingUsuarioItem> ListaRanking = new List<RankingUsuarioItem>();
        List<RankingUsuarioItem> listaAMostrar { get; set; } = new List<RankingUsuarioItem>();

        [Inject]
		public IPuntajeService puntajeService { get; set; }

		protected override async Task OnInitializedAsync()
		{
			ListaRanking = await puntajeService.GetListaRankingUsuarios();
            CantPaginasTotales = (ListaRanking.Count() / CantAMostrar) == 0 ? 1 : (ListaRanking.Count() / CantAMostrar);
            listaAMostrar = Paginar(PaginaActual);
        }

        public List<RankingUsuarioItem> Paginar(int numPagina)
        {
           var lista =  ListaRanking
                .OrderByDescending(a => a.PuntajeTotal)
                .Skip((numPagina - 1) * CantAMostrar)
                .Take(CantAMostrar)
                .ToList();
            return lista;
        }

        public void CambiaAPaginaAnterior()
        {
            if (PaginaActual == 1)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Estas en la primer página", Severity.Normal, config => { config.Icon = Icons.Material.Outlined.Warning; });
                return;
            }
            else
            {
                PaginaActual -= 1;
                listaAMostrar = Paginar(PaginaActual);
            }
        }

        public void CambiaAPaginaSiguiente()
        {

            if (PaginaActual == CantPaginasTotales)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("Estas en la última página", Severity.Normal, config => { config.Icon = Icons.Material.Outlined.Warning; });
                return;
            }
            else
            {
                PaginaActual += 1;
                listaAMostrar = Paginar(PaginaActual);
            }
        }
    }
}
