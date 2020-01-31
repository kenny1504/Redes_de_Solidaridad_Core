using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Turnos
    {
        public Turnos()
        {
            Matriculas = new HashSet<Matriculas>();
        }

        public uint Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Matriculas> Matriculas { get; set; }
    }
}
