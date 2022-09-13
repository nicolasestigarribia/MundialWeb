using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class Jugador
    {
        public int IdJugador { get; set; }
        public int IdEquipo { get; set; }
        public int GolesFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
        public int VallasInvictas { get; set; }
        public string Puesto { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }

        public Jugador(int idJugador, int idEquipo, int golesFavor, int golesEnContra, int tarjetasAmarillas, int tarjetasRojas, int vallasInvictas, string puesto, string nombre)
        {
            IdJugador = idJugador;
            IdEquipo = idEquipo;
            GolesFavor = golesFavor;
            GolesEnContra = golesEnContra;
            TarjetasAmarillas = tarjetasAmarillas;
            TarjetasRojas = tarjetasRojas;
            VallasInvictas = vallasInvictas;
            Puesto = puesto;
            Nombre = nombre;
        }
    }
}
