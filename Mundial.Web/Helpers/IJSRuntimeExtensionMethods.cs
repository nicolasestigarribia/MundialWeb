using Microsoft.JSInterop;

namespace Mundial.Web.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        //Clase para la implementacion de javascript en blazor
        //inicializador del timer para el deslogueo automatico(ver en wwwrooot js utilities.js)/
        public static async ValueTask InitializeInactivityTimer<T>(this IJSRuntime js, DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("InitializeInactivityTimer", dotNetObjectReference);
        }


        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
         => js.InvokeAsync<object>(
         "SessionStorage.setItem",
         key, content
         );

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);


    }
}
