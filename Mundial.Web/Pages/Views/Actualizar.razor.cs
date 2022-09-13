using Microsoft.AspNetCore.Components;
using Mundial.Web.Services;

namespace Mundial.Web.Pages.Views
{
	public partial class Actualizar
	{
        public int Value { get; set; }
        public int BufferValue { get; set; }
        int cantRegistros = 0;

        [Inject]
        private IPuntajeService puntajeService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {

        }

        protected async Task BtnActualizarAsync()
        {

            await Task.Run(async () =>
            {
                cantRegistros = await puntajeService.ActualizarPuntajes();
            });
            SimulateProgress();

        }
        public async void SimulateProgress()
        {
            Value = 5;
            BufferValue = 10;
            do
            {
                if (_disposed)
                {
                    return;
                }

                Value += 4;
                BufferValue += 5;
                StateHasChanged();
                await Task.Delay(500);
                if(cantRegistros != 0)
                {
                    Value = 100;
                    BufferValue = 100;
                }

            } while (Value < 100);
            StateHasChanged();
        }

        private bool _disposed;
        public void Dispose() => _disposed = true;
    }
}
