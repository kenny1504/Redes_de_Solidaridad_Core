using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Detallenotas
    {
        public Detallenotas()
        {
            Notas = new HashSet<Notas>();
        }

        public uint Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Notas> Notas { get; set; }
    }
}
