using Mundial.Entidades.LoginModel;
using Mundial.Entidades;

namespace Mundial.Web.Services
{
	public interface IUsuarioService
	{
        Task<bool> Insert(UsuarioInsertRequestDto respuestaNueva);
        Task<Usuario> GetById(int id);
        //Task<bool> Update(Usuario usuario);
        Task<Usuario> GetByEmail(string mail);
        Task<Usuario> GetByNickName(string nick);

    }
}
