using Mundial.Entidades.LoginModel;
namespace Mundial.Web.Services
{
    public interface ILoginService
    {
        Task<bool> Login(LoginRequestDto userLogin);

        Task<string> GetTokenFromAPI(LoginRequestDto userLogin);

        Task LogOut();

    }
}
