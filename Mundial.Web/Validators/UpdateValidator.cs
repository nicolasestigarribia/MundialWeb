using FluentValidation;
using Mundial.Entidades;
using Mundial.Web.Services;

namespace Mundial.Web.Validators
{
	public class UpdateValidator : AbstractValidator<PerfilItem>
	{
        private IUsuarioService usuarioServicio { get; set; }

        public UpdateValidator(IUsuarioService usuarioService)
		{
			this.usuarioServicio = usuarioService;
            RuleFor(a => a.Email).Cascade(CascadeMode.Stop)
                                               .NotEmpty().EmailAddress().WithMessage("Dirección de mail incorrecta")
                                               .MustAsync(VerificarSiExisteEmail)
                                               .WithMessage("Email ya registrado");
        }

        private async Task<bool> VerificarSiExisteEmail(string mail, CancellationToken arg2)
        {
            var existe = await usuarioServicio.GetByEmail(mail);
            return existe.IdUsuario == 0;
        }
    }
}
