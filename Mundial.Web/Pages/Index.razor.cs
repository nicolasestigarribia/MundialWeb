using Microsoft.AspNetCore.Components;
using Mundial.Entidades;
using Mundial.Entidades.Twitter;
using Mundial.Web.Services;

namespace Mundial.Web.Pages
{
    public partial class Index
    {

        private List<RankingUsuarioItem> ListaRanking = new List<RankingUsuarioItem>();

        private List<Tweet> listaTweets = new List<Tweet>();

        private List<Jugador> ListaJugadores = new List<Jugador>();

        private User userTwitter = new User();


        [Inject]
        private IPuntajeService PuntajeService { get; set; }

        [Inject]
        private ITwitterService TwitterService { get; set; }
        [Inject]
        private IUsuarioService UsuarioService { get; set; }
        [Inject]
        private IJugadorService JugadorService { get; set; }
        [Inject]
        private ISessionStorageService sessionStorageService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var idUser = await sessionStorageService.GetItemAsync<string>("ID");
            var ranking = await PuntajeService.GetListaRankingUsuarios();
            ListaRanking.AddRange(ranking.Take(10));

            listaTweets = await TwitterService.GetAllTweet();
            userTwitter = await TwitterService.GetUser();
            ListaJugadores = (await JugadorService.GetJugadoresPorGoles()).ToList();
            

        }
    }
}
