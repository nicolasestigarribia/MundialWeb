using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class LogVisita
    {
        public int IdLog { get; set; }
        public DateTime Fecha { get; set; }
        public int IdAccion { get; set; }
        public string Nick { get; set; }
        public string Clave { get; set; }
        public string UserHostIp { get; set; }
        public string UserPlatform { get; set; }
        public string UserBrowser { get; set; }
    }
}
