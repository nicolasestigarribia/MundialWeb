using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
	public class JugadorPuntajeItem
	{

        public Jugador Jugador { get; set; }
        public JugadorPuntaje Puntajes { get; set; }

        public int PuntajeGolAFavor { get; set; }
        public int PuntajeGolEnContra { get; set; }
        public int PuntajeTarjetaAmarilla { get; set; }
        public int PuntajeTarjetaRoja { get; set; }
        public int PuntajeVallaInvicta { get; set; }

        public string Nombre { get { return Jugador == null ? "Sin Definir" : Jugador.Nombre; } }
        public string Puesto { get { return Jugador == null ? "Sin Definir" : Jugador.Puesto; } }


        public int GolesFavor { get { return Jugador == null ? 0 : Jugador.GolesFavor; } }
        public int GolesEncontra { get { return Jugador == null ? 0 : Jugador.GolesEnContra; } }
        public int TarjetasRojas { get { return Jugador == null ? 0 : Jugador.TarjetasRojas; } }
        public int TarjetasAmarillas { get { return Jugador == null ? 0 : Jugador.TarjetasAmarillas; } }
        public int VallasInvictas { get { return Jugador == null ? 0 : Jugador.VallasInvictas; } }

        public int PuntajeTotal { get { return PuntajeGolAFavor + PuntajeGolEnContra + PuntajeTarjetaAmarilla + PuntajeTarjetaRoja + PuntajeVallaInvicta; } }

        public JugadorPuntajeItem(Jugador jugador, JugadorPuntaje puntajes)
        {
            this.Jugador = jugador;
            this.Puntajes = puntajes;

            this.PuntajeGolEnContra = Jugador.GolesEnContra * Puntajes.GolEnContra;
            this.PuntajeTarjetaAmarilla = Jugador.TarjetasAmarillas * Puntajes.TarjetaAmarilla;
            this.PuntajeTarjetaRoja = Jugador.TarjetasRojas * Puntajes.TarjetaRoja;

            // Puntajes particulares según el Puesto
            switch (Jugador.Puesto)
            {
                case "Arquero":
                    PuntajeGolAFavor = Jugador.GolesFavor * Puntajes.GolAfavorArq;
                    PuntajeVallaInvicta = Jugador.VallasInvictas * Puntajes.VallaInvictaArq;
                    break;

                case "Defensor":
                    PuntajeGolAFavor = Jugador.GolesFavor * Puntajes.GolAfavorDef;
                    PuntajeVallaInvicta = Jugador.VallasInvictas * Puntajes.VallaInvictaDef;
                    break;

                case "Volante":
                    PuntajeGolAFavor = Jugador.GolesFavor * Puntajes.GolAfavorVol;
                    PuntajeVallaInvicta = 0;
                    break;

                case "Delantero":
                    PuntajeGolAFavor = Jugador.GolesFavor * Puntajes.GolAfavorDel;
                    PuntajeVallaInvicta = 0;
                    break;
            }

        }

    }
}
