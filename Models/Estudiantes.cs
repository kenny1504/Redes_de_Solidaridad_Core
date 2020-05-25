using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Estudiantes
    {
        public string CodigoEstudiante { get; set; }
        public int PersonasId { get; set; }
        public int ParentescosId { get; set; }
        public int Id { get; set; }
        public int TutorId { get; set; }
    }
}
