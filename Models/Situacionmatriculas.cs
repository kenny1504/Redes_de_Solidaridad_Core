using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Situacionmatriculas
    {
        public Situacionmatriculas()
        {
            Matriculas = new HashSet<Matriculas>();
        }

        public uint Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Matriculas> Matriculas { get; set; }
    }
}
