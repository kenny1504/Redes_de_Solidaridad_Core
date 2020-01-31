using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Matriculas
    {
        public Matriculas()
        {
            Detallematriculas = new HashSet<Detallematriculas>();
        }

        public uint Id { get; set; }
        public DateTime Fecha { get; set; }
        public uint Ofertaid { get; set; }
        public uint Turnoid { get; set; }
        public uint SituacionMatriculaid { get; set; }
        public uint Estudianteid { get; set; }

        public virtual Estudiantes Estudiante { get; set; }
        public virtual Ofertas Oferta { get; set; }
        public virtual Situacionmatriculas SituacionMatricula { get; set; }
        public virtual Turnos Turno { get; set; }
        public virtual ICollection<Detallematriculas> Detallematriculas { get; set; }
    }
}
