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

    //Clase para mostrar lista de estudiantes (ADMINISTRADOR)
    public class EstudiantesADMIN
    {
        public string Institucion { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }
    }

    //Clase para mostrar Datos de estudiante (ADMINISTRADOR)
    public class DatosEstudiantesADMIN
    {
        public string Institucion { get; set; }
        public string Nombre { get; set; }
        public string CodigoEstudiante { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Tutor { get; set; }
        public int TelefonoTutor { get; set; }
        public int Grado { get; set; }
        public string Grupo { get; set; }
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

        public int Idestudiante { get; set; }
        public int IdMatricula { get; set; }
        public int Idgrado { get; set; }
        public int idGrupo { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }

    }
}
