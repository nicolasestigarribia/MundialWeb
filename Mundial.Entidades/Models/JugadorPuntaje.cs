using System;
using System.Collections.Generic;

#nullable disable

namespace Mundial.Entidades
{
    public partial class JugadorPuntaje
    {
        public int Id { get; set; }
        public int GolAfavorArq { get; set; }
        public int GolAfavorDef { get; set; }
        public int GolAfavorVol { get; set; }
        public int GolAfavorDel { get; set; }
        public int GolEnContra { get; set; }
        public int TarjetaAmarilla { get; set; }
        public int TarjetaRoja { get; set; }
        public int VallaInvictaArq { get; set; }
        public int VallaInvictaDef { get; set; }



        /// <summary>
        /// Evalua los goles, tarjetas y vallas invictas de un jugador calculando
        /// su puntaje en base a los parámetros definidos en esta clase.
        /// </summary>
        /// <param name="jugador">Jugador a evaluar.</param>
        /// <returns>Puntaje total del jugador.</returns>
        public short PuntuarJugador(Jugador jugador)
        {
            int puntaje = 0;
            // Puntajes generales independientes del Puesto
            puntaje += jugador.GolesEnContra * this.GolEnContra;
            puntaje += jugador.TarjetasAmarillas * this.TarjetaAmarilla;
            puntaje += jugador.TarjetasRojas * this.TarjetaRoja;

            // Puntajes particulares según el Puesto
            switch (jugador.Puesto)
            {
                case "Arquero":
                    puntaje += jugador.GolesFavor * this.GolAfavorArq;
                    puntaje += jugador.VallasInvictas * this.VallaInvictaArq;
                    break;

                case "Defensor":
                    puntaje += jugador.GolesFavor * this.GolAfavorDef;
                    puntaje += jugador.VallasInvictas * this.VallaInvictaDef;
                    break;

                case "Volante":
                    puntaje += jugador.GolesFavor * this.GolAfavorVol;
                    break;

                case "Delantero":
                    puntaje += jugador.GolesFavor * this.GolAfavorDel;
                    break;
            }

            return (short)puntaje;
        }

        /// <summary>
        /// Dado una lista de jugadores correspondientes a un equipo evalua los goles, 
        /// tarjetas y vallas invictas de cada jugador calculando su puntaje en base a
        /// los parámetros definidos en esta clase.
        /// </summary>
        /// <param name="equipo">Una lista de Jugadores.</param>
        /// <returns>Puntaje total del Equipo.</returns>
        public short PuntuarEquipo(List<Jugador> equipo)
        {
            short puntaje = 0;
            foreach (var jugador in equipo)
            {
                puntaje += this.PuntuarJugador(jugador);
            }
            return puntaje;
        }
    }
}
