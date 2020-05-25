using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Ofertas
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaOferta { get; set; }
        public int SeccionesId { get; set; }
        public int GradoAcademicoId { get; set; }
        public int GruposId { get; set; }
        public int DocentesId { get; set; }
    }
}
