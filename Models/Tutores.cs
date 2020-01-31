using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Tutores
    {
        public Tutores()
        {
            Estudiantes = new HashSet<Estudiantes>();
        }

        public uint Id { get; set; }
        public uint Personasid { get; set; }
        public uint Oficiosid { get; set; }

        public virtual Oficios Oficios { get; set; }
        public virtual Personas Personas { get; set; }
        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
    }
}
