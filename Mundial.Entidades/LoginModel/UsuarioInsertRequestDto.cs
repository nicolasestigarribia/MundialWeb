using Mundial.Entidades.Enum;

namespace Mundial.Entidades.LoginModel
{
    public class UsuarioInsertRequestDto
    {
	    public string Nombre { get; set; }
	    public string Apellido { get; set; }
	    public int Legajo { get; set; }
		public string Cuit { get; set; }
        public string Nickname { get; set; }
		public string Password { get; set; }
		public string Mail {get; set; }
		public int TipoUsuario { get; set; }
		public int IdEmpresa { get; set; }
		
    }
}
