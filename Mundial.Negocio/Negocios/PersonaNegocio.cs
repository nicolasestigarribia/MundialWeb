using Mundial.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Negocio
{
	public class PersonaNegocio : BaseNegocio<Persona>
	{
		public PersonaNegocio()
		{
		}

		public int Insertar(Persona personaNew)
		{
			Context.Personas.Add(personaNew);
			return Context.SaveChanges();
		}

		public int GetLastID()
		{
			return Context.Personas.Max(a => a.IdPersona);
		}

		public PerfilItem GetById(int idPersona)
		{
			var query = from persona in Context.Personas
						join user in Context.Usuarios on new { persona.IdPersona } equals new { user.IdPersona}
						join empresa in Context.Empresas on new { persona.IdEmpresa } equals new { empresa.IdEmpresa }
						where persona.IdPersona == idPersona
						select new PerfilItem
						{
							Puntaje = user.Puntaje,
							Empresa = empresa,
							NickName = user.NickName,
							Email = user.Mail,
							Nombre = persona.Nombre,
							IdUsuario = user.IdUsuario,
							Apellido = persona.Apellido,
							Cuit = persona.Cuit,
							IdClub = persona.IdClub,
							IdDeporte = persona.IdDeporte,
							IdEmpresa = persona.IdEmpresa,
							IdHobby = persona.IdHobby,
							IdPersona = persona.IdPersona,
							Imagen = persona.Imagen,
							Interno = persona.Interno,
							Legajo = persona.Legajo,
							RedSocial = persona.RedSocial,
							Telefono = persona.Telefono,
						};
			return query.SingleOrDefault();
		}

        public PerfilItem GetByLegajo(int legajo)
        {
            var query = from persona in Context.Personas
                        join user in Context.Usuarios on new { persona.IdPersona } equals new { user.IdPersona }
                        join empresa in Context.Empresas on new { persona.IdEmpresa } equals new { empresa.IdEmpresa }
                        where persona.Legajo == legajo
                        select new PerfilItem
                        {
                            Puntaje = user.Puntaje,
                            Empresa = empresa,
                            NickName = user.NickName,
                            Email = user.Mail,
                            Nombre = persona.Nombre,
                            IdUsuario = user.IdUsuario,
                            Apellido = persona.Apellido,
                            Cuit = persona.Cuit,
                            IdClub = persona.IdClub,
                            IdDeporte = persona.IdDeporte,
                            IdEmpresa = persona.IdEmpresa,
                            IdHobby = persona.IdHobby,
                            IdPersona = persona.IdPersona,
                            Imagen = persona.Imagen,
                            Interno = persona.Interno,
                            Legajo = persona.Legajo,
                            RedSocial = persona.RedSocial,
                            Telefono = persona.Telefono,
                        };
            return query.SingleOrDefault();
        }
        public PerfilItem GetByCuit(string cuit)
        {
            var query = from persona in Context.Personas
                        join user in Context.Usuarios on new { persona.IdPersona } equals new { user.IdPersona }
                        join empresa in Context.Empresas on new { persona.IdEmpresa } equals new { empresa.IdEmpresa }
                        where persona.Cuit == cuit
                        select new PerfilItem
                        {
                            Puntaje = user.Puntaje,
                            Empresa = empresa,
                            NickName = user.NickName,
                            Email = user.Mail,
                            Nombre = persona.Nombre,
                            IdUsuario = user.IdUsuario,
                            Apellido = persona.Apellido,
                            Cuit = persona.Cuit,
                            IdClub = persona.IdClub,
                            IdDeporte = persona.IdDeporte,
                            IdEmpresa = persona.IdEmpresa,
                            IdHobby = persona.IdHobby,
                            IdPersona = persona.IdPersona,
                            Imagen = persona.Imagen,
                            Interno = persona.Interno,
                            Legajo = persona.Legajo,
                            RedSocial = persona.RedSocial,
                            Telefono = persona.Telefono,
                        };
            return query.SingleOrDefault();
        }

    }
}
