using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace Mundial.Web.Auth
{
    public class AuthenticationProvider : AuthenticationStateProvider
    {

        private readonly HttpClient HttpClient;
        public static string TOKEN_KEY = "TOKEN_KEY";
        public ISessionStorageService SesionStorage;

        public AuthenticationProvider(ISessionStorageService SesionStorage, HttpClient httpClient/*, IJSRuntime js*/)
        {
            this.HttpClient = httpClient;
            this.SesionStorage = SesionStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await SesionStorage.GetItemAsync<string>(TOKEN_KEY);
            AuthenticationState result = new AuthenticationState(new ClaimsPrincipal());

            if (!string.IsNullOrEmpty(token))
            {
                result = ConstruirAuthenticationState(token);
            }
            
                NotifyAuthenticationStateChanged(Task.FromResult(result));
          
            return await Task.FromResult(result);
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsFromToken().ClaimsUsuario(token)));
        }

        public async Task NotificarLogOut()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        public async Task NotificarLogin(string token)
        {
            var authState = ConstruirAuthenticationState(token);
            await SesionStorage.SetItemAsync("IdPersona", authState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value.Trim());
            await SesionStorage.SetItemAsync("Nickname", authState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value.Trim());
            await SesionStorage.SetItemAsync("ID", authState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor)!.Value.Trim());
            await SesionStorage.SetItemAsync("Rol", authState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value.Trim());
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
    }
}
