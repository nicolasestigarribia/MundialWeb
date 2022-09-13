using Mundial.EF;
using Mundial.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Mundial.Negocio
{
    public class PersonalNegocio :  BaseNegocio<Personal>
    {

        public PersonalNegocio()
        {

        }

        public Personal GetById(int idEmpresa, int legajoDNI)
        {
            return Context.Personals.FirstOrDefault(a => a.IdEmpresa == idEmpresa && a.Legajo == legajoDNI);
        }


        public void AltaUsuarioPersonal (Personal persona, Usuario usuario)
        {
			var personaBD = GetById(persona.IdEmpresa, persona.Legajo);
			if(personaBD != null && personaBD.IdUsuario == 0)
            {
				var usuarioNegocio = new UsuarioNegocio();
                if (usuarioNegocio.Insert(usuario))
                {
					persona.IdUsuario = usuarioNegocio.GetLastID();
					Update(persona);
				}
            }
        }

        public void AltaUsuarioProveedor(Personal persona, Usuario usuario)
        {
            
            if(GetByCondition(a => a.Nick == persona.Nick) == null && 
                GetByCondition(a => a.IdEmpresa == persona.IdEmpresa && a.Legajo == persona.Legajo) == null)
            {
                var usuarioNegocio = new UsuarioNegocio();
                if (usuarioNegocio.Insert(usuario))
                {
                    persona.IdUsuario = usuarioNegocio.GetLastID();
                    Insert(persona);
                }
            }            
        }

        public List<Personal> GetListaInscriptos()
        {
            return GetAllByCondition(a => a.IdUsuario != 0).ToList();
        }

        public List<Personal> GetListaInscriptosByEmpresa(int idEmpresa)
        {
            return GetAllByCondition(a => a.IdUsuario != 0 && a.IdEmpresa == idEmpresa).ToList();
        }
    }
}
