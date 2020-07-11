using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redes_De_Solidaridad.Models
{
    public class DasboardWS
    {
        public int Instituciones { get; set; }
        public int Docentes { get; set; }
        public int Estudiantes { get; set; }
        public int Matriculas { get; set; }
    }
    public class DasboarDocente
    {
        public string Institucione { get; set; }
        public string Grupo { get; set; }
        public string Grado { get; set; }
    }
}
