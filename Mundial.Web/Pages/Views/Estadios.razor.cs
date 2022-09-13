using Microsoft.AspNetCore.Components;
using Mundial.Entidades;
using Mundial.Web.Services;

namespace Mundial.Web.Pages.Views
{
    public partial class Estadios
    {
        public IEnumerable<Estadio> ListaEstadios { get; set; }

        [Inject]
        private IEstadioService estadioService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListaEstadios = await estadioService.GetAll();
        }
    }
}
