using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Secciones
    {
        public Secciones()
        {
            Ofertas = new HashSet<Ofertas>();
        }

        public uint Id { get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<Ofertas> Ofertas { get; set; }
    }
}
