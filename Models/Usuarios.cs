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
    public class usuariosview //Manejo de datos de usuario en sesion
    {
        public string NombreDeUsuario { get; set; }
        public string Nombre { get; set; }
        public string Id { get; set; }
        public int tipo { get; set; }
        public String Cedula { get; set; }
        public String Institucion { get; set; }
        public int Id_Institucion { get; set; }
    }
    public class usuarioDocenteview //Agrega Docente y edita  
    {
        public int Id { get; set; }
        public String Cedula { get; set; }
        public string Usuario { get; set; }
        public String Contraseña { get; set; }
        public int Institucion { get; set; }
    }
    public class userview //Inicio de sesion
    {
        public string password { get; set; }
        public string username { get; set; }
    }
    public class usuariosWS //Manejo de datos Usuarios en el  WS
    {
        public string NombreDeUsuario { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }
        public int tipo { get; set; }
        public string Cedula { get; set; }
        public string Institucion { get; set; }
    }

    public class Busqueda//Recive el id de la isntitucion
    {
        public int Id { get; set; }
    }
    public class BusquedaUD//Recive el id de la isntitucion
    {
        public string Cedula { get; set; }
    }
}
