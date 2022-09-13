using Mundial.Entidades.Twitter;

namespace Mundial.Web.Services
{
    public interface ITwitterService
    {
        Task<List<Tweet>> GetAllTweet();
        Task<User> GetUser();
    }
}
