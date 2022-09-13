using Mundial.Entidades.LoginModel;
using Mundial.Web.Auth;
using Newtonsoft.Json.Linq;
using System.Net;


namespace Mundial.Web.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient httpClient;

        public static string TOKEN_KEY = "TOKEN_KEY";

        public readonly ISessionStorageService SesionStorage;

        public readonly AuthenticationProvider authenticationProvider;

        public readonly ILogger<LoginService> logger;

        private string EndPoint { get; set; }


        public LoginService(HttpClient httpClient, ISessionStorageService SesionStorage, AuthenticationProvider authenticationProvider, ILogger<LoginService> logger)
        {
            this.httpClient = httpClient;
            this.SesionStorage = SesionStorage;
            this.authenticationProvider = authenticationProvider;
            this.logger = logger;
            EndPoint = "api/Login";
        }
        public async Task<string> GetTokenFromAPI(LoginRequestDto userLogin)
        {
            string result = string.Empty;
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{EndPoint}", userLogin);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(token.Result);
                    result = json["token"].ToString();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error al obtener el JWT", e.Message);
            }
            return result;
        }

        public async Task<bool> Login(LoginRequestDto userLogin)
        {
            try
            {
                var token = await GetTokenFromAPI(userLogin);
                if (!string.IsNullOrEmpty(token))
                {
                    await GuardarTokenYAutenticar(token);
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Login -> Error al intentar autenticar : {mensaje}", e.Message);
            }
            return false;
        }

        private async Task GuardarTokenYAutenticar(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {

                await SesionStorage.SetItemAsync(TOKEN_KEY, token);
                await authenticationProvider.NotificarLogin(token);
            }
        }
        public async Task LogOut()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            await SesionStorage.RemoveItemAsync(TOKEN_KEY);
            await authenticationProvider.NotificarLogOut();
        }

    }
}
