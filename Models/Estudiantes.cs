using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Estudiantes
    {
        public string CodigoEstudiante { get; set; }
        public int PersonasId { get; set; }
        public int ParentescosId { get; set; }
        public int Id { get; set; }
        public int TutorId { get; set; }
    }
    public class estudianteWS
    {
        public int IdInstitucion{ get; set; }
        public int IdGrado { get; set; }
        public int IdGrupo { get; set; }
        public int IdEstudiante { get; set; }

    }
    public class DatosWS
    {
        public string Nombre { get; set; }
        public string CodigoEstudiante { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Tutor { get; set; }
        public int TelefonoTutor { get; set; }

    }
    public class ListaEstudiantesWS
    {
        public string Nombre { get; set; }
        public string Sexo { get; set; }

    }
}
