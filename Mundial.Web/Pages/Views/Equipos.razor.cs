using Microsoft.AspNetCore.Components;
using Mundial.Entidades;
using Mundial.Web.Services;

namespace Mundial.Web.Pages.Views
{
    public partial class Equipos
    {
        public IEnumerable<Equipo> ListaEquipos { get; set; }

        [Inject]
        private IEquipoService IEquipoService { get; set; }

        public Dictionary<string, List<Equipo>> EquiposAgrupados { get; set; }

        public List<string> ListaGrupos { get; set; }


        protected override async Task OnInitializedAsync()
        {
            ListaEquipos = await IEquipoService.GetAll();
            ListaGrupos = ListaEquipos.Select(a => a.Grupo).Distinct().ToList();
            CargaEquiposAgrupados();
        }

        private void CargaEquiposAgrupados()
        {
            EquiposAgrupados = new Dictionary<string, List<Equipo>>();
            foreach (var grupo in ListaGrupos)
            {
                EquiposAgrupados.Add(grupo.Trim(), ListaEquipos.Where(a => a.Grupo == grupo).ToList());
            }
        }

    }
}
