using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Notas
    {
        public uint Id { get; set; }
        public uint Nota { get; set; }
        public uint DetalleNotaid { get; set; }
        public uint DetalleMatriculaid { get; set; }

        public virtual Detallematriculas DetalleMatricula { get; set; }
        public virtual Detallenotas DetalleNota { get; set; }
    }
}
