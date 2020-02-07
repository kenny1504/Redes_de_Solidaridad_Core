using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Usuarios
    {
        public int IdUsuarios { get; set; }
        public string ClaveDeUsuario { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string NombreDeUsuario { get; set; }
    }
    class  usuariosview
    {
        public string ClaveDeUsuario { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Nombre{ get; set; }
        public string Cedula { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaDeVencimiento { get; set; }
        public Boolean login_in { get; set; }


    }
    public class userview
    {
        public string password { get; set; }
        public string username { get; set; }

    }
}
