using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Grupos
    {
        public int Id { get; set; }
        public string Grupo { get; set; }
    }
    public class Gruposws // clase que Retorna Datos
    {
        public int Idgrupo { get; set; }
        public int Cantidad { get; set; }
        public string Grupo { get; set; }
    }
    public class Grupos_ws // clase que recive Datos
    {
        public int institucion { get; set; }
        public int Grado { get; set; }
    }
}
