using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Gradoaasignaturas
    {
        public uint Id { get; set; }
        public uint Gradoid { get; set; }
        public uint Asignaturaid { get; set; }

        public virtual Asignaturas Asignatura { get; set; }
        public virtual Grados Grado { get; set; }
    }
}
