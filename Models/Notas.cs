using System;
using System.Collections.Generic;

namespace Redes_De_Solidaridad.Models
{
    public partial class Notas
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int DetalleNotaId { get; set; }
        public int DetalleMatriculaId { get; set; }
    }
    public class Notas_Estudiante
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public String Nombre { get; set; }
    }
    public class BusquedaNota
    {
        public int IdInstitucion { get; set; }
        public int IdGrado { get; set; }
        public int IdGrupo { get; set; }
        public int IdAsignatura { get; set; }
        public int IdDetalleNota { get; set; }
    }

    public class AgregarNota
    {
        public int IdMatricula { get; set; }
        public int IdDetalle { get; set; }
        public int IdAsigntura { get; set; }
        public int Nota { get; set; }
    }

    public class VerNotaDocente
    {
        public string cedula { get; set; }
        public int id_detalle_Nota { get; set; }
        public int idMateria { get; set; }
    }

    public class NotasD
    {
        public int[] IdNota { get; set; }
        public int[] Nota { get; set; }
    }

}
