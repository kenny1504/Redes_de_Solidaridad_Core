using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Usuariosinstituciones
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int IdInstitucion { get; set; }
    }

    public class UsuarioInstitucion
    {
        public int Id { get; set; }
        public string Institucion { get; set; }
        public string Direcccion { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }

    }
}
