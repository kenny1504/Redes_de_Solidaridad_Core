using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Detallematriculas
    {
        public Detallematriculas()
        {
            Notas = new HashSet<Notas>();
        }

        public uint Id { get; set; }
        public uint Asignaturaid { get; set; }
        public uint Matriculaid { get; set; }

        public virtual Asignaturas Asignatura { get; set; }
        public virtual Matriculas Matricula { get; set; }
        public virtual ICollection<Notas> Notas { get; set; }
    }
}
