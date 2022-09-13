using System;
using System.Collections.Generic;

namespace Mundial.Entidades
{
    public partial class Persona
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Telefono { get; set; }
        public int Legajo { get; set; }
        public string Cuit { get; set; } = null!;
        public int IdEmpresa { get; set; }
        public int? IdHobby { get; set; }
        public int? IdDeporte { get; set; }
        public int? IdClub { get; set; }
        public string? RedSocial { get; set; }
        public byte[]? Imagen { get; set; }
        public int IdPersona { get; set; }
        public int? Interno { get; set; }

    }
}
