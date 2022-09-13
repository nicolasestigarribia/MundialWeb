using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class Personal
    {
        public int IdEmpresa { get; set; }
        public int? IdUsuario { get; set; }
        public int CentroCosto { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public string Cuit { get; set; }
        public bool Revalida { get; set; }
        public bool? Activo { get; set; }
        public string Sector { get; set; }
        public string NivelEstudios { get; set; }
        public string EstadoEstudios { get; set; }
        public string TituloCertificacion { get; set; }
        public string HijosEdadEscolar { get; set; }
    }
}
