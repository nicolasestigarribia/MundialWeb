using FluentValidation;
using Mundial.Entidades;
using Mundial.Entidades.LoginModel;
using Mundial.Web.Services;

namespace Mundial.Web.Validators
{
	public class RegistroValidator : AbstractValidator<UsuarioInsertRequestDto>
	{
        private IUsuarioService usuarioServicio { get; set; }
        private IPersonaService personaService { get; set; }
        private IProveedorService proveedorService { get; set; }

        public RegistroValidator(IUsuarioService usuarioService, IPersonaService personaService, IProveedorService proveedorService)
		{
            this.usuarioServicio = usuarioService;
            this.personaService = personaService;
            this.proveedorService = proveedorService;
            RuleFor(usuario => usuario.Nombre).Cascade(CascadeMode.Stop)
                                         .NotEmpty()
                                         .WithMessage("El nombre del usuario no puede ser un espacio en blanco")
                                         .Length(0, 50)
                                         .WithMessage("El nombre del usuario debe tener entre 1 y 50 caracteres");
            RuleFor(usuario => usuario.Apellido).Cascade(CascadeMode.Stop)
                             .NotEmpty()
                             .WithMessage("El apellido del usuario no puede ser un espacio en blanco")
                             .Length(0, 50)
                             .WithMessage("El apellido del usuario debe tener entre 1 y 50 caracteres");
            RuleFor(usuario => usuario.Nickname).Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .WithMessage("El nickname del usuario no puede ser un espacio en blanco")
                 .Length(0, 50)
                 .WithMessage("El nickname del usuario debe tener entre 1 y 50 caracteres")
                 .MustAsync(VerificarSiExisteNickname).WithMessage("Usuario ya registrado");
            RuleFor(a => a.Legajo).MustAsync(VerificarSiExisteLegajo).WithMessage("Legajo ya registrado");
            RuleFor(usuario => usuario.Cuit).Cascade(CascadeMode.Stop)
                            .Length(0, 11)
                            .WithMessage("El cuit del usuario debe tener entre 1 y 11 caracteres")
                            .MustAsync(VerificarSiExisteCuit).WithMessage("El cuit ya esta registrado")
                            .MustAsync(VerificarSiExisteElProveedor).WithMessage("El proveedor no esta registrado en la empresa");
            
            RuleFor(a => a.Mail).Cascade(CascadeMode.Stop)
                                               .NotEmpty().EmailAddress().WithMessage("Dirección de mail incorrecta")
                                               .MustAsync(VerificarSiExisteEmail)
                                               .WithMessage("Email ya registrado");
            RuleFor(a => a.IdEmpresa).NotEmpty().WithMessage("Ingrese la empresa a la que pertenece el usuario");
        }
        private async Task<bool> VerificarSiExisteEmail(string mail, CancellationToken arg2)
        {
            var existe = await usuarioServicio.GetByEmail(mail);
            return existe.IdUsuario == 0;
        }
        private async Task<bool> VerificarSiExisteNickname(string nickName, CancellationToken arg2)
        {
            var existe = await usuarioServicio.GetByNickName(nickName);
            return existe.IdUsuario == 0;
        }
        private async Task<bool> VerificarSiExisteLegajo(int legajo, CancellationToken arg2)
        {
            var existe = await personaService.GetBylegajo(legajo);
            return existe.IdPersona == 0;
        }
        private async Task<bool> VerificarSiExisteCuit(string cuit, CancellationToken arg2)
        {
            var existe = await personaService.GetByCuit(cuit);
            return existe.IdUsuario == 0;
        }

        private async Task<bool> VerificarSiExisteElProveedor(string cuit, CancellationToken arg2)
        {
            var existe = await proveedorService.GetByCuit(cuit);
            return existe.IdProveedor > 0;
        }


    }
}
