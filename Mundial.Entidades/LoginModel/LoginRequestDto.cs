using System.ComponentModel.DataAnnotations;

namespace Mundial.Entidades.LoginModel
{ 
    public class LoginRequestDto
    {
        public string Nickname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
