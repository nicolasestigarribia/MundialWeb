using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public class PerfilItem : Persona
    {
        public int IdUsuario { get; set; }

        public string Email { get; set; } = null!;

        public string NickName { get; set; } = null!;

        public int Puntaje { get; set; }

        public Empresa Empresa { get; set; }

        public PerfilItem()
        {
            Empresa = new Empresa();
            NickName = String.Empty;
            IdUsuario = 0;
            Puntaje = 0;
            Email = String.Empty;
        }

    }
}
