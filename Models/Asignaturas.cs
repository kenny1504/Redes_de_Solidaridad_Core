using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Asignaturas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public  class AsignaturasBusqueda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
