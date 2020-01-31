using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Asignaturas
    {
        public Asignaturas()
        {
            Detallematriculas = new HashSet<Detallematriculas>();
            Gradoaasignaturas = new HashSet<Gradoaasignaturas>();
        }

        public uint Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Detallematriculas> Detallematriculas { get; set; }
        public virtual ICollection<Gradoaasignaturas> Gradoaasignaturas { get; set; }
    }
}
