
using Mundial.Entidades;
using Mundial.Entidades.Twitter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Mundial.Web.Services
{
    public class TwitterService : ITwitterService
    {

        private readonly HttpClient httpClient;
        private readonly ILogger<TwitterService> logger;
        private string EndPoint { get; set; }
        private string Bearer { get; set; }

        private IConfiguration Builder { get; set; }

        public TwitterService(HttpClient httpClient, ILogger<TwitterService> logger, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            this.EndPoint = "users/2541583021";
            this.Builder = configuration;
        }

        public async Task<List<Tweet>> GetAllTweet()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Builder.GetSection("Bearer:Twitter").Value);
            RootTw? myDeserializedClass = new RootTw();
            try
            {
                var response = await httpClient.GetAsync($"{EndPoint}/tweets");
                myDeserializedClass = JsonConvert.DeserializeObject<RootTw>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetAllTweets -> Error al intentar obtener los tweets", ex.Message);
            }
            return myDeserializedClass.data;
        }

        public async Task<User> GetUser()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Builder.GetSection("Bearer:Twitter").Value);
            User? user = new User();
            try
            {
                var response = await httpClient.GetAsync($"{EndPoint}?user.fields=profile_image_url");
                var root = JsonConvert.DeserializeObject<RootUser>(response.Content.ReadAsStringAsync().Result);
                user = root.data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetUser -> Error al intentar obtener el usuario", ex.Message);
            }
            return user;
        }
    }
}
