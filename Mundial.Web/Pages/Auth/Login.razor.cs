using Microsoft.AspNetCore.Components;
using Mundial.Entidades.LoginModel;
using Mundial.Web.Services;

namespace Mundial.Web.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        private bool _processing = false;
        private LoginRequestDto model { get; set; } = new LoginRequestDto();

        [Inject]
        public ILoginService IloginService { get; set; }


        protected async Task LoginUser()
        {
            if (await IloginService.Login(model))
            {
                navegador.NavigateTo("/index");
            }
        }

        async Task ProcessSomething()
        {
            _processing = true;
            await LoginUser();
            _processing = false;
        }
    }
}
