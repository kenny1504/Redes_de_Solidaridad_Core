using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Ofertas
    {
        public Ofertas()
        {
            Matriculas = new HashSet<Matriculas>();
        }

        public uint Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaOferta { get; set; }
        public uint Seccionid { get; set; }
        public uint Gradoid { get; set; }
        public uint Grupoid { get; set; }
        public uint Docenteid { get; set; }

        public virtual Docentes Docente { get; set; }
        public virtual Grados Grado { get; set; }
        public virtual Grupos Grupo { get; set; }
        public virtual Secciones Seccion { get; set; }
        public virtual ICollection<Matriculas> Matriculas { get; set; }
    }

    public class OfertaView
    {
        public string NombreDocente { get; set; }
        public string DescripcionOferta { get; set; }
    }
}
