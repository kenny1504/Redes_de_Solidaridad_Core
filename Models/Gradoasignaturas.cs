using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Gradoasignaturas
    {
        public int Id { get; set; }
        public int GradoAcademicoId { get; set; }
        public int AsignaturasId { get; set; }
    }
}
