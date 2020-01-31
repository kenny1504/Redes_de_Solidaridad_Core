using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Grupos
    {
        public Grupos()
        {
            Ofertas = new HashSet<Ofertas>();
        }

        public uint Id { get; set; }
        public string Grupo { get; set; }

        public virtual ICollection<Ofertas> Ofertas { get; set; }
    }
}
