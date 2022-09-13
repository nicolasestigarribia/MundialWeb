using System;
using System.Collections.Generic;

namespace Mundial.Entidades
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int Puntaje { get; set; }
        public string Mail { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public bool Activo { get; set; }
        public int TipoUsuario { get; set; }
        public int IdPersona { get; set; }
    }
}
