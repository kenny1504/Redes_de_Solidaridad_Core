using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Notas
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int DetalleNotaId { get; set; }
        public int DetalleMatriculaId { get; set; }
    }
}
