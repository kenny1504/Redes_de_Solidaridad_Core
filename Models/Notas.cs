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
}
