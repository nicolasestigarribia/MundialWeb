using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundial.Entidades
{
    public class RankingUsuarioItem
    {

        public int CantidadDeAciertos { get; set; }
        public int PuntajeTotal { get; set; }
        public DateTime FechaGrabacion { get; set; }
        public Persona Persona { get; set; }
        public Usuario Usuario {get; set;}
        public string EmpresaNombre { get; set; }
        public int Posicion { get; set; }

        public RankingUsuarioItem(int cantidadDeAciertos, DateTime fechaGrabacion,string empresa, Usuario usuario, Persona persona)
        {
            this.CantidadDeAciertos = cantidadDeAciertos;
            this.FechaGrabacion = fechaGrabacion;
            this.Usuario = usuario;
            this.Persona = persona;
            this.EmpresaNombre = empresa;
        }

        public RankingUsuarioItem()
        {
        }

    }
}
