using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Oficios
    {
        public Oficios()
        {
            Tutores = new HashSet<Tutores>();
        }

        public uint Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Tutores> Tutores { get; set; }
    }
}
