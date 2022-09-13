using System.Security.Claims;
using System.Text.Json;
namespace Mundial.Web.Auth
{
    public class ClaimsFromToken
    {

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public ClaimsIdentity ClaimsUsuario(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var ClaimsUser = new ClaimsIdentity(new[]
			{
                new Claim(ClaimTypes.Actor, identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor)!.Value),
                new Claim(ClaimTypes.Name, identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value),
                new Claim(ClaimTypes.NameIdentifier, identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value),
                new Claim(ClaimTypes.Role, identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value)
            }
            , "apiAuth_type") ;

            return ClaimsUser;
        }
    }
}
