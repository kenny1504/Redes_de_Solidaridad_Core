using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Detallematriculainstitucion
    {
        public int Id { get; set; }
        public int IdInstitucion { get; set; }
        public int IdMatricula { get; set; }
    }
}
