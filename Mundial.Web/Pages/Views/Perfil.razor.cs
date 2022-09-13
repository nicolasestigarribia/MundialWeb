using Mundial.Web.Services;
using Mundial.Entidades;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;
using Mundial.Web.Validators;
using FluentValidation.Results;

namespace Mundial.Web.Pages.Views
{
	public partial class Perfil
	{
        bool Editar = true;
        public PerfilItem PerfilUser { get; set; } = new PerfilItem();
        public Usuario Usuario { get; set; } = new Usuario();
        public List<Hobby> ListaHobbys { get; set; } = new List<Hobby>();
        public List<Deporte> ListaDeportes { get; set; } = new List<Deporte>();
        public List<Club> ListaClub { get; set; } = new List<Club>();
        public List<ValidationFailure> Errors { get; set; }= new List<ValidationFailure>();

        [Inject]
		public IUsuarioService UsuarioServicio { get; set; }

        [Inject]
        public IPersonaService PersonaService { get; set; }

        [Inject]
        public IHobbyService HobbyService { get; set; }

        [Inject]
        public IDeporteService DeporteService { get; set; }

        [Inject]
        public IClubService ClubService { get; set; }

        [Parameter]
		public int Id { get; set; }

        private int _profileValue { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (_profileValue != Id) //Comparamos el valor, y si es distinto, consultamos la información
            {
                await CargaVista();
                StateHasChanged();
            }
            await base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            Usuario = await UsuarioServicio.GetById(Id);
            ListaHobbys = await HobbyService.GetAll();
            ListaDeportes = await DeporteService.GetAll();
            ListaClub = await ClubService.GetAll();
        }

        public async Task CargaVista()
        {
            _profileValue = Id;
            PerfilUser = await PersonaService.GetById(Usuario.IdPersona);
            IdHobby = Convert.ToInt32(PerfilUser.IdHobby == null ? 0 : PerfilUser.IdHobby);
            IdClub = Convert.ToInt32(PerfilUser.IdClub == null ? 0 : PerfilUser.IdClub);
            IdDeporte = Convert.ToInt32(PerfilUser.IdDeporte == null ? 0 : PerfilUser.IdDeporte);
            Telefono = Convert.ToDouble(PerfilUser.Telefono);
            Email = PerfilUser.Email;
        }
        public async void Save()
        {
            UpdateValidator validationRules = new UpdateValidator(UsuarioServicio);
            bool flag = true;
            var result = new ValidationResult();

            PerfilUser.IdHobby = IdHobby;
            PerfilUser.IdClub = IdClub;
            PerfilUser.IdDeporte = IdDeporte;
            PerfilUser.Telefono = Telefono.ToString();

            if (PerfilUser.Email != Email)
            {
                PerfilUser.Email = Email;
                result = await validationRules.ValidateAsync(PerfilUser);
                if(!result.IsValid)
                {
                    flag = false;
                }
            }
            if(flag)
            {
                var aux = await PersonaService.Update(PerfilUser);
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
                if (aux)
                {
                    Snackbar.Add("¡ Se modifico el perfil correctamente !", Severity.Success);
                }
                else
                {
                    Snackbar.Add("¡ No se pudo modificar el perfil, compruebe los datos !", Severity.Normal);
                }
                Editar = true;
                StateHasChanged();
            }
            else
            {
                Errors.Clear();
                Errors.AddRange(result.Errors);
            }
            Editar = true;
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            var format = "image/*";
            try
            {
                if (e.GetMultipleFiles().Count > 0)
                {
                    var file = e.GetMultipleFiles().First();
                    using (var ms = new MemoryStream())
                    {
                        await file.OpenReadStream().CopyToAsync(ms);
                        string s = ms.ToString();
                        byte[] fileBytes = ms.ToArray();
                        this.PerfilUser.Imagen = fileBytes;
                        var aux = await PersonaService.Update(PerfilUser);
                        if (aux)
                        {
                            Snackbar.Add("¡ Se actualizo su foto correctamente !", Severity.Success);
                        }
                        else
                        {
                            Snackbar.Add("¡ No se pudo modificar su foto !", Severity.Normal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("¡ Tamaño de imagen incorrecto, este debe ser menor a 50Kb !", Severity.Error);
                throw;
            }
        }

    }
}
