using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Estudiantes
    {
        public Estudiantes()
        {
            Matriculas = new HashSet<Matriculas>();
        }

        public uint Id { get; set; }
        public uint Personasid { get; set; }
        public uint CodigoEstudiante { get; set; }
        public uint Parentescoid { get; set; }
        public uint Tutorid { get; set; }

        public virtual Parentescos Parentesco { get; set; }
        public virtual Personas Personas { get; set; }
        public virtual Tutores Tutor { get; set; }
        public virtual ICollection<Matriculas> Matriculas { get; set; }
    }
}
