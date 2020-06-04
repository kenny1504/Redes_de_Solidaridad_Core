using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Gradoacademico
    {
        public int Id { get; set; }
        public int Grado { get; set; }
    }
    public class Estudiantes_grado
    {
        public int Grado { get; set; }
        public int Cantidad { get; set; }
    }
}
