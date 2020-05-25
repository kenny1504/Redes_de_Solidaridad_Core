using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Matriculas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int OfertasId { get; set; }
        public int TurnoId { get; set; }
        public int SituacionMatriculaId { get; set; }
        public int EstudiantesId { get; set; }
    }
}
