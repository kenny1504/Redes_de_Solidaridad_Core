using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Parentescos
    {
        public Parentescos()
        {
            Estudiantes = new HashSet<Estudiantes>();
        }

        public uint Id { get; set; }
        public string Parentesco { get; set; }

        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
    }
}
