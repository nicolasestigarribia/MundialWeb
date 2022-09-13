using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class CentrosCosto
    {
        public int IdEmpresa { get; set; }
        public int CentroCosto { get; set; }
        public string Nombre { get; set; }
        public short CentroCostoPadre { get; set; }
        public bool? ParticipaEnGrupos { get; set; }
        public int CantidadDeEmpleados { get; set; }
    }
}
