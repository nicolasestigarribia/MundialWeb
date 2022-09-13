using Mundial.Entidades;
using Mundial.Entidades.LoginModel;
using Mundial.Negocio.Utils;

namespace Mundial.Negocio
{
    public class UsuarioNegocio : BaseNegocio<Usuario>
    {
        public UsuarioNegocio() 
        {
            
        }


        public bool Registrar(UsuarioInsertRequestDto userNuevo, Usuario user)
        {
            PasswordHash.Create(userNuevo.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.TipoUsuario = user.TipoUsuario;
            Context.Usuarios.Add(user);
            return  Context.SaveChanges() > 0;
        }

        public void Login()
        {

        }

        public int GetLastID()
        {
            return Context.Usuarios.Max(a => a.IdUsuario);

        }

        public bool Exist(string nickName)
        {
            return Context.Usuarios.Count(p => p.NickName.Trim().Contains(nickName.Trim())) > 0;
        }


        public int ActualizaPuntajesUsuario()
        {

            var listaRankingUsuarios = (from UserRta in Context.UsuariosRespuestas
                                        join User in Context.Usuarios on new { UserRta.IdUsuario } equals new { User.IdUsuario }
                                        group new {User, UserRta, UserRta.Puntaje }
                                        by new
                                        {
                                            User.IdUsuario
                                        } into userAgrupados
                                        select new Usuario()
                                        {
                                            Puntaje = userAgrupados.Sum(a => a.Puntaje),
                                            IdUsuario = userAgrupados.Select(a => a.User.IdUsuario).SingleOrDefault(),
                                            Mail = userAgrupados.Select(a => a.User.Mail).SingleOrDefault(),
                                            PasswordHash = userAgrupados.Select(a => a.User.PasswordHash).SingleOrDefault(),
                                            PasswordSalt = userAgrupados.Select(a => a.User.PasswordSalt).SingleOrDefault(),
                                            Activo = userAgrupados.Select(a => a.User.Activo).SingleOrDefault(),
                                            IdPersona = userAgrupados.Select(a => a.User.IdPersona).SingleOrDefault() ,
                                            NickName = userAgrupados.Select(a => a.User.NickName).SingleOrDefault(),
                                            TipoUsuario = userAgrupados.Select(a => a.User.TipoUsuario).SingleOrDefault()
                                        }).ToList();

            foreach (var usuario in listaRankingUsuarios)
            {
                Context.Usuarios.Update(usuario);
            }
            return Context.SaveChanges();
        }

    }
}
