using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int IdInstitucion { get; set; }
    }

    class usuariosview
    {
        public string NombreDeUsuario { get; set; }
        public string Nombre { get; set; }
        public string Id { get; set; }
        public int tipo { get; set; }
        public String Cedula { get; set; }
        public String Institucion { get; set; }
    }
    public class userview
    {
        public string password { get; set; }
        public string username { get; set; }

    }
}
