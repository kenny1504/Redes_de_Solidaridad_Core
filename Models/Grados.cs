using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Grados
    {
        public Grados()
        {
            Gradoaasignaturas = new HashSet<Gradoaasignaturas>();
            Ofertas = new HashSet<Ofertas>();
        }

        public uint Id { get; set; }
        public uint Grado { get; set; }

        public virtual ICollection<Gradoaasignaturas> Gradoaasignaturas { get; set; }
        public virtual ICollection<Ofertas> Ofertas { get; set; }
    }
}
