using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Mundial.Entidades;
using Mundial.Entidades.Enum;
using Mundial.Entidades.LoginModel;
using Mundial.Web.Services;
using Mundial.Web.Validators;

namespace Mundial.Web.Pages.Views
{
	public partial class Registro
	{
		protected UsuarioInsertRequestDto usuarioInsert { get; set; } = new UsuarioInsertRequestDto();

		protected List<Empresa> ListaEmpresas { get; set; } = new List<Empresa>();

        public List<ValidationFailure> Errors = new List<ValidationFailure>();

        protected bool seRegistro { get; set; } = false;

		[Inject]
		private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private IEmpresaService EmpresaService { get; set; }
        [Inject]
        private IPersonaService personaService { get; set; }
		[Inject]
        private IProveedorService proveedorService { get; set; }

        [Inject]
		private NavigationManager navigation { get; set; }

		protected override async Task OnInitializedAsync()
		{
           ListaEmpresas = (await EmpresaService.GetAll()).ToList(); 
		}

		protected async void RegistrarUser()
		{
			if(usuarioInsert.IdEmpresa == 5)
			{
				usuarioInsert.TipoUsuario = ((int)TipoUsuario.PROVEEDOR);
				usuarioInsert.Apellido = "Proveedor";
			}else
			{
				usuarioInsert.TipoUsuario = ((int)TipoUsuario.PERSONAL);
            }
			RegistroValidator validationRules = new RegistroValidator(UsuarioService, personaService, proveedorService);
			var result = await validationRules.ValidateAsync(usuarioInsert);

			if(result.IsValid)
			{
                seRegistro = await UsuarioService.Insert(usuarioInsert);
                if (seRegistro)
                {
                    Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
                    Snackbar.Add("¡ Usted se registro correctamente !", Severity.Success);
                    await Task.Delay(1300);
                    navigation.NavigateTo("/", true);
                }
			}
			else
			{
				Errors.Clear();

                Errors.AddRange(result.Errors);
				StateHasChanged();
            }
        }
		protected void GoToInicio()
		{
            navigation.NavigateTo("/", true);

		}
	}
}
