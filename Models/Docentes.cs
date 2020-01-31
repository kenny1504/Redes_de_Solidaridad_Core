using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Docentes
    {
        public Docentes()
        {
            Ofertas = new HashSet<Ofertas>();
        }

        public uint Id { get; set; }
        public uint Personasid { get; set; }
        public bool Estado { get; set; }

        public virtual Personas Personas { get; set; }
        public virtual ICollection<Ofertas> Ofertas { get; set; }
    }
}
